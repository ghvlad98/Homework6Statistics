namespace Samples
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chart = new System.Windows.Forms.PictureBox();
            this.drawMeans = new System.Windows.Forms.Button();
            this.drawVariances = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chart.Location = new System.Drawing.Point(3, 34);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1222, 483);
            this.chart.TabIndex = 0;
            this.chart.TabStop = false;
            // 
            // drawMeans
            // 
            this.drawMeans.Location = new System.Drawing.Point(887, 5);
            this.drawMeans.Name = "drawMeans";
            this.drawMeans.Size = new System.Drawing.Size(140, 23);
            this.drawMeans.TabIndex = 1;
            this.drawMeans.Text = "Draw Means";
            this.drawMeans.UseVisualStyleBackColor = true;
            this.drawMeans.Click += new System.EventHandler(this.drawMeans_Click);
            // 
            // drawVariances
            // 
            this.drawVariances.Location = new System.Drawing.Point(1076, 5);
            this.drawVariances.Name = "drawVariances";
            this.drawVariances.Size = new System.Drawing.Size(140, 23);
            this.drawVariances.TabIndex = 2;
            this.drawVariances.Text = "Draw Variances";
            this.drawVariances.UseVisualStyleBackColor = true;
            this.drawVariances.Click += new System.EventHandler(this.drawVariances_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 519);
            this.Controls.Add(this.drawVariances);
            this.Controls.Add(this.drawMeans);
            this.Controls.Add(this.chart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox chart;
        private Button drawMeans;
        private Button drawVariances;
    }
}