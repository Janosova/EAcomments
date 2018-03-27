namespace EAcomments
{
    partial class ImportWindow
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
            this.filePathLabel = new System.Windows.Forms.Label();
            this.filePathField = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.openFileBrowser = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // filePathLabel
            // 
            this.filePathLabel.AutoSize = true;
            this.filePathLabel.Location = new System.Drawing.Point(12, 21);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(62, 17);
            this.filePathLabel.TabIndex = 0;
            this.filePathLabel.Text = "File path";
            // 
            // filePathField
            // 
            this.filePathField.Location = new System.Drawing.Point(107, 21);
            this.filePathField.Name = "filePathField";
            this.filePathField.Size = new System.Drawing.Size(316, 22);
            this.filePathField.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(452, 20);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(452, 63);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 3;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // openFileBrowser
            // 
            this.openFileBrowser.FileName = "openFileBrowser";
            this.openFileBrowser.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileBrowser_FileOk);
            // 
            // ImportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 107);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.filePathField);
            this.Controls.Add(this.filePathLabel);
            this.Name = "ImportWindow";
            this.Text = "ImportWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImportWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label filePathLabel;
        private System.Windows.Forms.TextBox filePathField;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileBrowser;
    }
}