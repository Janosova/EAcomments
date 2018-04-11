namespace EAcomments
{
    partial class ExportWindow
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
            this.browseButton = new System.Windows.Forms.Button();
            this.filePathField = new System.Windows.Forms.TextBox();
            this.fileNameField = new System.Windows.Forms.TextBox();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.filePathLabel = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.saveFileBrowser = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(401, 93);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // filePathField
            // 
            this.filePathField.Location = new System.Drawing.Point(100, 93);
            this.filePathField.Name = "filePathField";
            this.filePathField.Size = new System.Drawing.Size(275, 22);
            this.filePathField.TabIndex = 1;
            this.filePathField.TextChanged += new System.EventHandler(this.FileFieldsChanged);
            // 
            // fileNameField
            // 
            this.fileNameField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fileNameField.Location = new System.Drawing.Point(100, 31);
            this.fileNameField.Name = "fileNameField";
            this.fileNameField.Size = new System.Drawing.Size(376, 22);
            this.fileNameField.TabIndex = 2;
            this.fileNameField.TextChanged += new System.EventHandler(this.FileFieldsChanged);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(21, 34);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(73, 17);
            this.fileNameLabel.TabIndex = 3;
            this.fileNameLabel.Text = "File name:";
            // 
            // filePathLabel
            // 
            this.filePathLabel.AutoSize = true;
            this.filePathLabel.Location = new System.Drawing.Point(21, 96);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(66, 17);
            this.filePathLabel.TabIndex = 4;
            this.filePathLabel.Text = "File path:";
            // 
            // exportButton
            // 
            this.exportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(401, 144);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            // 
            // saveFileBrowser
            // 
            this.saveFileBrowser.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileBrowser_FileOk);
            // 
            // ExportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 179);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.filePathLabel);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.fileNameField);
            this.Controls.Add(this.filePathField);
            this.Controls.Add(this.browseButton);
            this.Name = "ExportWindow";
            this.Text = "ExportWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox filePathField;
        private System.Windows.Forms.TextBox fileNameField;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label filePathLabel;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.SaveFileDialog saveFileBrowser;
    }
}