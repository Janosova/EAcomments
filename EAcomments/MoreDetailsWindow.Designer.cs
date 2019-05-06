namespace EAcomments
{
    partial class MoreDetailsWindow
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
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.IssueTypeLabel = new System.Windows.Forms.Label();
            this.LastModifiedLabel = new System.Windows.Forms.Label();
            this.AuthorTextBox = new System.Windows.Forms.TextBox();
            this.IssueTypeTextBox = new System.Windows.Forms.TextBox();
            this.LastModifiedTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(12, 18);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(41, 13);
            this.AuthorLabel.TabIndex = 0;
            this.AuthorLabel.Text = "Author:";
            // 
            // IssueTypeLabel
            // 
            this.IssueTypeLabel.AutoSize = true;
            this.IssueTypeLabel.Location = new System.Drawing.Point(12, 51);
            this.IssueTypeLabel.Name = "IssueTypeLabel";
            this.IssueTypeLabel.Size = new System.Drawing.Size(62, 13);
            this.IssueTypeLabel.TabIndex = 1;
            this.IssueTypeLabel.Text = "Issue Type:";
            // 
            // LastModifiedLabel
            // 
            this.LastModifiedLabel.AutoSize = true;
            this.LastModifiedLabel.Location = new System.Drawing.Point(12, 87);
            this.LastModifiedLabel.Name = "LastModifiedLabel";
            this.LastModifiedLabel.Size = new System.Drawing.Size(60, 13);
            this.LastModifiedLabel.TabIndex = 2;
            this.LastModifiedLabel.Text = "Created At:";
            this.LastModifiedLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // AuthorTextBox
            // 
            this.AuthorTextBox.Location = new System.Drawing.Point(94, 15);
            this.AuthorTextBox.Name = "AuthorTextBox";
            this.AuthorTextBox.ReadOnly = true;
            this.AuthorTextBox.Size = new System.Drawing.Size(210, 20);
            this.AuthorTextBox.TabIndex = 3;
            // 
            // IssueTypeTextBox
            // 
            this.IssueTypeTextBox.Location = new System.Drawing.Point(94, 48);
            this.IssueTypeTextBox.Name = "IssueTypeTextBox";
            this.IssueTypeTextBox.ReadOnly = true;
            this.IssueTypeTextBox.Size = new System.Drawing.Size(210, 20);
            this.IssueTypeTextBox.TabIndex = 4;
            // 
            // LastModifiedTextBox
            // 
            this.LastModifiedTextBox.Location = new System.Drawing.Point(94, 84);
            this.LastModifiedTextBox.Name = "LastModifiedTextBox";
            this.LastModifiedTextBox.ReadOnly = true;
            this.LastModifiedTextBox.Size = new System.Drawing.Size(210, 20);
            this.LastModifiedTextBox.TabIndex = 5;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(229, 124);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MoreDetailsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 171);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.LastModifiedTextBox);
            this.Controls.Add(this.IssueTypeTextBox);
            this.Controls.Add(this.AuthorTextBox);
            this.Controls.Add(this.LastModifiedLabel);
            this.Controls.Add(this.IssueTypeLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Name = "MoreDetailsWindow";
            this.Text = "MoreDetailsWindow";
            this.Load += new System.EventHandler(this.MoreDetailsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label IssueTypeLabel;
        private System.Windows.Forms.Label LastModifiedLabel;
        private System.Windows.Forms.TextBox AuthorTextBox;
        private System.Windows.Forms.TextBox IssueTypeTextBox;
        private System.Windows.Forms.TextBox LastModifiedTextBox;
        private System.Windows.Forms.Button OkButton;
    }
}