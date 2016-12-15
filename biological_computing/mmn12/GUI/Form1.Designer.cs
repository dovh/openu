﻿namespace ApplicationSpace
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
            this.InitializeButton = new System.Windows.Forms.Button();
            this.StepButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectionRangeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LocalMinimumDetectionTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MinimumFitnessTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RunningTimeTextBox = new System.Windows.Forms.TextBox();
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
            this.ExitButton.Location = new System.Drawing.Point(760, 389);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(760, 317);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // CrossoverProbabiltyTextBox
            // 
            this.CrossoverProbabiltyTextBox.Location = new System.Drawing.Point(763, 16);
            this.CrossoverProbabiltyTextBox.Name = "CrossoverProbabiltyTextBox";
            this.CrossoverProbabiltyTextBox.Size = new System.Drawing.Size(72, 20);
            this.CrossoverProbabiltyTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Crossover Probabilioty:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(649, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mutation Probabilioty:";
            // 
            // MutationProbabilityTextBox
            // 
            this.MutationProbabilityTextBox.Location = new System.Drawing.Point(763, 52);
            this.MutationProbabilityTextBox.Name = "MutationProbabilityTextBox";
            this.MutationProbabilityTextBox.Size = new System.Drawing.Size(72, 20);
            this.MutationProbabilityTextBox.TabIndex = 5;
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(760, 353);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 7;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // InitializeButton
            // 
            this.InitializeButton.Location = new System.Drawing.Point(760, 245);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 8;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // StepButton
            // 
            this.StepButton.Location = new System.Drawing.Point(760, 281);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(75, 23);
            this.StepButton.TabIndex = 9;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(656, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Selection range (%):";
            // 
            // SelectionRangeTextBox
            // 
            this.SelectionRangeTextBox.Location = new System.Drawing.Point(763, 88);
            this.SelectionRangeTextBox.Name = "SelectionRangeTextBox";
            this.SelectionRangeTextBox.Size = new System.Drawing.Size(72, 20);
            this.SelectionRangeTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(599, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Local minimum detection period:";
            // 
            // LocalMinimumDetectionTextBox
            // 
            this.LocalMinimumDetectionTextBox.Location = new System.Drawing.Point(763, 124);
            this.LocalMinimumDetectionTextBox.Name = "LocalMinimumDetectionTextBox";
            this.LocalMinimumDetectionTextBox.Size = new System.Drawing.Size(72, 20);
            this.LocalMinimumDetectionTextBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(673, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Minimum fitness:";
            // 
            // MinimumFitnessTextBox
            // 
            this.MinimumFitnessTextBox.Enabled = false;
            this.MinimumFitnessTextBox.Location = new System.Drawing.Point(763, 159);
            this.MinimumFitnessTextBox.Name = "MinimumFitnessTextBox";
            this.MinimumFitnessTextBox.Size = new System.Drawing.Size(72, 20);
            this.MinimumFitnessTextBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(673, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Running time:";
            // 
            // RunningTimeTextBox
            // 
            this.RunningTimeTextBox.Enabled = false;
            this.RunningTimeTextBox.Location = new System.Drawing.Point(763, 194);
            this.RunningTimeTextBox.Name = "RunningTimeTextBox";
            this.RunningTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.RunningTimeTextBox.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 430);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RunningTimeTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MinimumFitnessTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LocalMinimumDetectionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectionRangeTextBox);
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
        private System.Windows.Forms.Button InitializeButton;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SelectionRangeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LocalMinimumDetectionTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MinimumFitnessTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RunningTimeTextBox;
    }
}

