using System.Runtime.Intrinsics.X86;

namespace Samples
{
    public partial class Form1 : Form
    {
        Image img;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Tuple<double[], double[]>? getData(List<Dictionary<string, double>> population, string variable, int m, int n)
        {
           if (!population[0].ContainsKey(variable))
            {
                MessageBox.Show("Variable not found!");
                return null;
            }
            else
            {
                Random ran = new Random();
                List<double[]> samples = new List<double[]>();
                while (samples.Count < m)
                {
                    double[] sample = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        int randIndex = ran.Next(population.Count);
                        Dictionary<string, double> randItem = population.ElementAt(randIndex);
                        sample[i] = randItem[variable];
                    }
                    samples.Add(sample);
                }

                double[] means = new double[m];
                double[] variances = new double[m];
                for (int i = 0; i < m; i++)
                {
                    double tot = samples[i].Sum();
                    double avg = tot / n;
                    means[i] = avg;
                    double sumDiff = (from num in samples[i] select Math.Pow(num - avg, 2.0)).Sum();
                    variances[i] = sumDiff / n - 1;
                }
                
                return new Tuple<double[], double[]>(means, variances);
            }
        }

        private Tuple<double, double> getMinMax(List<Dictionary<string, double>> population, string variable)
        {
            double minValue = population[0][variable];
            double maxValue = population[0][variable];
            for (int i = 1; i < population.Count; i++)
            {
                if (population[i][variable] < minValue)
                {
                    minValue = population[i][variable];
                }

                if (population[i][variable] > maxValue)
                {
                    maxValue = population[i][variable];
                }
            }

            return new Tuple<double, double>(minValue, maxValue);
        }

        private void drawAvg(List<Dictionary<string, double>> population, string variable, int m, int n)
        {
            double minX = 0;
            double maxX = m;
            double minY = 0;
            double maxY = getMinMax(population, variable).Item2;
            double[] means = getData(population, variable, m, n).Item1;

            Pen pen =  new Pen(Color.Orange);
            List<Point> path = new List<Point>();
            for (int i = 0; i < m; i++)
            {
                int newX = convertX(i + 1, minX, maxX, chart.Width);
                int newY = convertY(means[i], minY, maxY, chart.Height);
                path.Add(new Point(newX, newY));
            }
            g.DrawLines(pen, path.ToArray());

        }

        private void drawVar(List<Dictionary<string, double>> population, string variable, int m, int n)
        {
            double range = getMinMax(population, variable).Item2 - getMinMax(population, variable).Item1;
            double maxVar = Math.Pow(range, 2.0) / 4;

            double minX = 0;
            double maxX = m;
            double minY = 0;
            double maxY = maxVar;
            double[] variances = getData(population, variable, m, n).Item2;

            Pen pen = new Pen(Color.Orange);
            List<Point> path = new List<Point>();
            for (int i = 0; i < m; i++)
            {
                int newX = convertX(i + 1, minX, maxX, chart.Width);
                int newY = convertY(variances[i], minY, maxY, chart.Height);
                path.Add(new Point(newX, newY));
            }
            g.DrawLines(pen, path.ToArray());
        }

        private int convertX(double x, double minX, double maxX, int w)
        {
            if (maxX - minX == 0)
            {
                return 0;
            }

            return (int)(w * (x - minX) / (maxX - minX));
        }

        private int convertY(double y, double minY, double maxY, int h) 
        {
            if (maxY - minY == 0) 
            {
                return 0;
            }

            return (int)(h - h * (y - minY) / (maxY - minY));
        }

        private void calculate(List<Dictionary<string, double>> population, string variable, int m, int n, string type)
        {
            double sum = 0;
            foreach (Dictionary<string, double> item in population)
            {
                sum += item[variable];
            }

            double popMean = sum / population.Count;
            double sumDiff = (from item in population select Math.Pow(item[variable] - popMean, 2.0)).Sum();
            double popVar = sumDiff / population.Count;

            if (type == "mean") 
            {
                double[] samMeans = getData(population, variable, m, n).Item1;
                double meanSamMeans = samMeans.Sum() / m;
                double diff1 = (from el in samMeans select Math.Pow(el - meanSamMeans, 2.0)).Sum();
                double varSamMeans = diff1 / m;

                MessageBox.Show("Population mean: " + popMean + ", Population variance: " + popVar +
                ", Mean of sample means: " + meanSamMeans + ", Variance of sample means: " + varSamMeans);
            }

            if (type == "variance")
            {
                double[] samVariances = getData(population, variable, m, n).Item2;
                double meanSamVariances = samVariances.Sum() / m;
                double diff2 = (from el in samVariances select Math.Pow(el - meanSamVariances, 2.0)).Sum();
                double varSamVariances = diff2 / m;

                MessageBox.Show("Population mean: " + popMean + ", Population variance: " + popVar +
                ", Mean of sample variances: " + meanSamVariances + ", Variance of sample variances: " + varSamVariances);
            }
        }

        private void drawMeans_Click(object sender, EventArgs e)
        {   
            chart.Image = new Bitmap(chart.Width, chart.Height);
            img = chart.Image;
            g = Graphics.FromImage(img);

            List<Dictionary<string, double>> population = new List<Dictionary<string, double>>();
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 185 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 60 }, { "Height", 165 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 179 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 90 }, { "Height", 182 }, { "Age", 26 } });
            population.Add(new Dictionary<string, double> { { "Weight", 78 }, { "Height", 167 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 175 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 85 }, { "Height", 183 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 175 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 74 }, { "Height", 188 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 64 }, { "Height", 170 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 69 }, { "Height", 169 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 73 }, { "Height", 173 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 180 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 70.5 }, { "Height", 179 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 141 }, { "Height", 229 }, { "Age", 26 } });
            population.Add(new Dictionary<string, double> { { "Weight", 71 }, { "Height", 187 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 76 }, { "Height", 181 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 72 }, { "Height", 178 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 74 }, { "Height", 172 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 70 }, { "Height", 180 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 67 }, { "Height", 172 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 51 }, { "Height", 165 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 60 }, { "Height", 168 }, { "Age", 24 } });
            population.Add(new Dictionary<string, double> { { "Weight", 85 }, { "Height", 180 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 99 }, { "Height", 176 }, { "Age", 24 } });
            population.Add(new Dictionary<string, double> { { "Weight", 65 }, { "Height", 180 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 70 }, { "Height", 184 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 68 }, { "Height", 170 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 73 }, { "Height", 180 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 68 }, { "Height", 175 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 74 }, { "Height", 172 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 65 }, { "Height", 177 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 48 }, { "Height", 164 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 90 }, { "Height", 187 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 85 }, { "Height", 195 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 72 }, { "Height", 170 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 65 }, { "Height", 172 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 76 }, { "Height", 180 }, { "Age", 17 } });

            drawAvg(population, "Age", 100, 3);
            calculate(population, "Age", 100, 3, "mean");
        }

        private void drawVariances_Click(object sender, EventArgs e)
        {
            chart.Image = new Bitmap(chart.Width, chart.Height);
            img = chart.Image;
            g = Graphics.FromImage(img);

            List<Dictionary<string, double>> population = new List<Dictionary<string, double>>();
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 185 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 60 }, { "Height", 165 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 179 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 90 }, { "Height", 182 }, { "Age", 26 } });
            population.Add(new Dictionary<string, double> { { "Weight", 78 }, { "Height", 167 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 175 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 85 }, { "Height", 183 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 175 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 74 }, { "Height", 188 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 64 }, { "Height", 170 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 69 }, { "Height", 169 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 73 }, { "Height", 173 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 80 }, { "Height", 180 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 70.5 }, { "Height", 179 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 141 }, { "Height", 229 }, { "Age", 26 } });
            population.Add(new Dictionary<string, double> { { "Weight", 71 }, { "Height", 187 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 76 }, { "Height", 181 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 72 }, { "Height", 178 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 74 }, { "Height", 172 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 70 }, { "Height", 180 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 67 }, { "Height", 172 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 51 }, { "Height", 165 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 60 }, { "Height", 168 }, { "Age", 24 } });
            population.Add(new Dictionary<string, double> { { "Weight", 85 }, { "Height", 180 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 99 }, { "Height", 176 }, { "Age", 24 } });
            population.Add(new Dictionary<string, double> { { "Weight", 65 }, { "Height", 180 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 70 }, { "Height", 184 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 68 }, { "Height", 170 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 73 }, { "Height", 180 }, { "Age", 25 } });
            population.Add(new Dictionary<string, double> { { "Weight", 68 }, { "Height", 175 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 74 }, { "Height", 172 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 65 }, { "Height", 177 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 48 }, { "Height", 164 }, { "Age", 21 } });
            population.Add(new Dictionary<string, double> { { "Weight", 90 }, { "Height", 187 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 85 }, { "Height", 195 }, { "Age", 23 } });
            population.Add(new Dictionary<string, double> { { "Weight", 72 }, { "Height", 170 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 65 }, { "Height", 172 }, { "Age", 22 } });
            population.Add(new Dictionary<string, double> { { "Weight", 76 }, { "Height", 180 }, { "Age", 17 } });

            drawVar(population, "Age", 100, 3);
            calculate(population, "Age", 100, 3, "variance");
        }
    }
}
