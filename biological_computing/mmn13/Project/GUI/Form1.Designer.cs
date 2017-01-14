namespace WindowsFormsApplication1
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
            this.NetworkGridView = new System.Windows.Forms.DataGridView();
            this.PreferencesGridView = new System.Windows.Forms.DataGridView();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.RandomizeButton = new System.Windows.Forms.Button();
            this.StepButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TotalHappinessTextBox = new System.Windows.Forms.TextBox();
            this.RunBatchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LegalSolutionTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.NetworkGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreferencesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // NetworkGridView
            // 
            this.NetworkGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.NetworkGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NetworkGridView.Location = new System.Drawing.Point(12, 12);
            this.NetworkGridView.Name = "NetworkGridView";
            this.NetworkGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.NetworkGridView.Size = new System.Drawing.Size(503, 433);
            this.NetworkGridView.TabIndex = 0;
            // 
            // PreferencesGridView
            // 
            this.PreferencesGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.PreferencesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PreferencesGridView.Location = new System.Drawing.Point(532, 12);
            this.PreferencesGridView.Name = "PreferencesGridView";
            this.PreferencesGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.PreferencesGridView.Size = new System.Drawing.Size(250, 250);
            this.PreferencesGridView.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(707, 452);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(381, 452);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 3;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(462, 452);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.Location = new System.Drawing.Point(626, 452);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(75, 23);
            this.RandomizeButton.TabIndex = 5;
            this.RandomizeButton.Text = "Initialize";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // StepButton
            // 
            this.StepButton.Location = new System.Drawing.Point(545, 451);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(75, 23);
            this.StepButton.TabIndex = 14;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(630, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Total Hapinness:";
            // 
            // TotalHappinessTextBox
            // 
            this.TotalHappinessTextBox.Location = new System.Drawing.Point(722, 279);
            this.TotalHappinessTextBox.Name = "TotalHappinessTextBox";
            this.TotalHappinessTextBox.ReadOnly = true;
            this.TotalHappinessTextBox.Size = new System.Drawing.Size(60, 20);
            this.TotalHappinessTextBox.TabIndex = 15;
            // 
            // RunBatchButton
            // 
            this.RunBatchButton.Location = new System.Drawing.Point(300, 452);
            this.RunBatchButton.Name = "RunBatchButton";
            this.RunBatchButton.Size = new System.Drawing.Size(75, 23);
            this.RunBatchButton.TabIndex = 17;
            this.RunBatchButton.Text = "RunBatch";
            this.RunBatchButton.UseVisualStyleBackColor = true;
            this.RunBatchButton.Click += new System.EventHandler(this.RunBatchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(630, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Legal solution:";
            // 
            // LegalSolutionTextBox
            // 
            this.LegalSolutionTextBox.Location = new System.Drawing.Point(722, 305);
            this.LegalSolutionTextBox.Name = "LegalSolutionTextBox";
            this.LegalSolutionTextBox.ReadOnly = true;
            this.LegalSolutionTextBox.Size = new System.Drawing.Size(60, 20);
            this.LegalSolutionTextBox.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 491);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LegalSolutionTextBox);
            this.Controls.Add(this.RunBatchButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TotalHappinessTextBox);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.RandomizeButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.PreferencesGridView);
            this.Controls.Add(this.NetworkGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.NetworkGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreferencesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView NetworkGridView;
        private System.Windows.Forms.DataGridView PreferencesGridView;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button RandomizeButton;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TotalHappinessTextBox;
        private System.Windows.Forms.Button RunBatchButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LegalSolutionTextBox;
    }
}

