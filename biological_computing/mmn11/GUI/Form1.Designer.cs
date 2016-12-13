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
            this.StepButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RandomSeepTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StepNoTextBox = new System.Windows.Forms.TextBox();
            this.NumSingelsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RunButton = new System.Windows.Forms.Button();
            this.TimerControl = new System.Windows.Forms.Timer(this.components);
            this.PauseButton = new System.Windows.Forms.Button();
            this.NumCouplesTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TotalHappinessTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ShowStagesCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MatchThresholdTextBox = new System.Windows.Forms.TextBox();
            this.InitializeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // StepButton
            // 
            this.StepButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.StepButton.Location = new System.Drawing.Point(739, 357);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(75, 23);
            this.StepButton.TabIndex = 0;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(706, 474);
            this.dataGridView.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ExitButton.Location = new System.Drawing.Point(739, 463);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RandomSeepTextBox
            // 
            this.RandomSeepTextBox.Location = new System.Drawing.Point(739, 204);
            this.RandomSeepTextBox.Name = "RandomSeepTextBox";
            this.RandomSeepTextBox.Size = new System.Drawing.Size(84, 20);
            this.RandomSeepTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(736, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Random Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(739, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Step No:";
            // 
            // StepNoTextBox
            // 
            this.StepNoTextBox.Location = new System.Drawing.Point(739, 27);
            this.StepNoTextBox.Name = "StepNoTextBox";
            this.StepNoTextBox.ReadOnly = true;
            this.StepNoTextBox.Size = new System.Drawing.Size(84, 20);
            this.StepNoTextBox.TabIndex = 6;
            // 
            // NumSingelsTextBox
            // 
            this.NumSingelsTextBox.Location = new System.Drawing.Point(739, 71);
            this.NumSingelsTextBox.Name = "NumSingelsTextBox";
            this.NumSingelsTextBox.ReadOnly = true;
            this.NumSingelsTextBox.Size = new System.Drawing.Size(84, 20);
            this.NumSingelsTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(739, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Number of Singels:";
            // 
            // RunButton
            // 
            this.RunButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.RunButton.Location = new System.Drawing.Point(739, 394);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 9;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // TimerControl
            // 
            this.TimerControl.Tick += new System.EventHandler(this.TimerControl_Tick);
            // 
            // PauseButton
            // 
            this.PauseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.PauseButton.Location = new System.Drawing.Point(739, 423);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 10;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // NumCouplesTextBox
            // 
            this.NumCouplesTextBox.Location = new System.Drawing.Point(739, 115);
            this.NumCouplesTextBox.Name = "NumCouplesTextBox";
            this.NumCouplesTextBox.ReadOnly = true;
            this.NumCouplesTextBox.Size = new System.Drawing.Size(84, 20);
            this.NumCouplesTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(739, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Number of Couples:";
            // 
            // TotalHappinessTextBox
            // 
            this.TotalHappinessTextBox.Location = new System.Drawing.Point(739, 161);
            this.TotalHappinessTextBox.Name = "TotalHappinessTextBox";
            this.TotalHappinessTextBox.ReadOnly = true;
            this.TotalHappinessTextBox.Size = new System.Drawing.Size(84, 20);
            this.TotalHappinessTextBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(739, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Total Happiness:";
            // 
            // ShowStagesCheckBox
            // 
            this.ShowStagesCheckBox.AutoSize = true;
            this.ShowStagesCheckBox.Location = new System.Drawing.Point(739, 284);
            this.ShowStagesCheckBox.Name = "ShowStagesCheckBox";
            this.ShowStagesCheckBox.Size = new System.Drawing.Size(113, 17);
            this.ShowStagesCheckBox.TabIndex = 15;
            this.ShowStagesCheckBox.Text = "Show inner stages";
            this.ShowStagesCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(736, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Match Threshold";
            // 
            // MatchThresholdTextBox
            // 
            this.MatchThresholdTextBox.Location = new System.Drawing.Point(739, 248);
            this.MatchThresholdTextBox.Name = "MatchThresholdTextBox";
            this.MatchThresholdTextBox.Size = new System.Drawing.Size(84, 20);
            this.MatchThresholdTextBox.TabIndex = 16;
            // 
            // InitializeButton
            // 
            this.InitializeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.InitializeButton.Location = new System.Drawing.Point(739, 318);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 18;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 506);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MatchThresholdTextBox);
            this.Controls.Add(this.ShowStagesCheckBox);
            this.Controls.Add(this.TotalHappinessTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NumCouplesTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.NumSingelsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StepNoTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RandomSeepTextBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "mmn11 - CA";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.TextBox RandomSeepTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StepNoTextBox;
        private System.Windows.Forms.TextBox NumSingelsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Timer TimerControl;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.TextBox NumCouplesTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TotalHappinessTextBox;
        private System.Windows.Forms.CheckBox ShowStagesCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MatchThresholdTextBox;
        private System.Windows.Forms.Button InitializeButton;
    }
}

