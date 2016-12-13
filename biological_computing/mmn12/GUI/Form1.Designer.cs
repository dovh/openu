namespace ApplicationSpace
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChartControl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.CrossoverProbabiltyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MutationProbabilityTextBox = new System.Windows.Forms.TextBox();
            this.PauseButton = new System.Windows.Forms.Button();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.InitializeButton = new System.Windows.Forms.Button();
            this.StepButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartControl
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartControl.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ChartControl.Legends.Add(legend1);
            this.ChartControl.Location = new System.Drawing.Point(12, 12);
            this.ChartControl.Name = "ChartControl";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.Name = "Average";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Max";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Min";
            this.ChartControl.Series.Add(series1);
            this.ChartControl.Series.Add(series2);
            this.ChartControl.Series.Add(series3);
            this.ChartControl.Size = new System.Drawing.Size(571, 405);
            this.ChartControl.TabIndex = 0;
            this.ChartControl.Text = "chart1";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(735, 394);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(735, 326);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // CrossoverProbabiltyTextBox
            // 
            this.CrossoverProbabiltyTextBox.Location = new System.Drawing.Point(738, 17);
            this.CrossoverProbabiltyTextBox.Name = "CrossoverProbabiltyTextBox";
            this.CrossoverProbabiltyTextBox.Size = new System.Drawing.Size(72, 20);
            this.CrossoverProbabiltyTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(618, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Crossover Probabilioty:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(618, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mutation Probabilioty:";
            // 
            // MutationProbabilityTextBox
            // 
            this.MutationProbabilityTextBox.Location = new System.Drawing.Point(738, 54);
            this.MutationProbabilityTextBox.Name = "MutationProbabilityTextBox";
            this.MutationProbabilityTextBox.Size = new System.Drawing.Size(72, 20);
            this.MutationProbabilityTextBox.TabIndex = 5;
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(735, 355);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 7;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // Timer
            // 
            this.Timer.Interval = 1;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // InitializeButton
            // 
            this.InitializeButton.Location = new System.Drawing.Point(735, 249);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 8;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // StepButton
            // 
            this.StepButton.Location = new System.Drawing.Point(735, 287);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(75, 23);
            this.StepButton.TabIndex = 9;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 430);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MutationProbabilityTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CrossoverProbabiltyTextBox);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.ChartControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartControl;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.TextBox CrossoverProbabiltyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MutationProbabilityTextBox;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Button InitializeButton;
        private System.Windows.Forms.Button StepButton;
    }
}

