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
            this.ATextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DTextBox = new System.Windows.Forms.TextBox();
            this.StepButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TotalHappinessTextBox = new System.Windows.Forms.TextBox();
            this.RunBatchButton = new System.Windows.Forms.Button();
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
            this.RandomizeButton.Text = "Randomize";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // ATextBox
            // 
            this.ATextBox.Location = new System.Drawing.Point(722, 274);
            this.ATextBox.Name = "ATextBox";
            this.ATextBox.Size = new System.Drawing.Size(60, 20);
            this.ATextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(664, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "A Factor: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(664, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "B Factor:";
            // 
            // BTextBox
            // 
            this.BTextBox.Location = new System.Drawing.Point(722, 300);
            this.BTextBox.Name = "BTextBox";
            this.BTextBox.Size = new System.Drawing.Size(60, 20);
            this.BTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(664, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "C Factor:";
            // 
            // CTextBox
            // 
            this.CTextBox.Location = new System.Drawing.Point(722, 326);
            this.CTextBox.Name = "CTextBox";
            this.CTextBox.Size = new System.Drawing.Size(60, 20);
            this.CTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(664, 359);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "D Factor:";
            // 
            // DTextBox
            // 
            this.DTextBox.Location = new System.Drawing.Point(722, 352);
            this.DTextBox.Name = "DTextBox";
            this.DTextBox.Size = new System.Drawing.Size(60, 20);
            this.DTextBox.TabIndex = 12;
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
            this.label5.Location = new System.Drawing.Point(630, 385);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Total Hapinness:";
            // 
            // TotalHappinessTextBox
            // 
            this.TotalHappinessTextBox.Location = new System.Drawing.Point(722, 378);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 491);
            this.Controls.Add(this.RunBatchButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TotalHappinessTextBox);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ATextBox);
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
        private System.Windows.Forms.TextBox ATextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox BTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DTextBox;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TotalHappinessTextBox;
        private System.Windows.Forms.Button RunBatchButton;
    }
}

