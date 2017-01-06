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
            this.InitializeButton = new System.Windows.Forms.Button();
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
            this.NetworkGridView.Size = new System.Drawing.Size(423, 364);
            this.NetworkGridView.TabIndex = 0;
            // 
            // PreferencesGridView
            // 
            this.PreferencesGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.PreferencesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PreferencesGridView.Location = new System.Drawing.Point(456, 126);
            this.PreferencesGridView.Name = "PreferencesGridView";
            this.PreferencesGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.PreferencesGridView.Size = new System.Drawing.Size(250, 250);
            this.PreferencesGridView.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(630, 410);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(387, 410);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 3;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(468, 410);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // InitializeButton
            // 
            this.InitializeButton.Location = new System.Drawing.Point(549, 410);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 5;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 458);
            this.Controls.Add(this.InitializeButton);
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

        }

        #endregion

        private System.Windows.Forms.DataGridView NetworkGridView;
        private System.Windows.Forms.DataGridView PreferencesGridView;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button InitializeButton;
    }
}

