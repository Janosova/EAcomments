namespace EAcomments
{
    partial class AddCommentWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtareaCommentText = new System.Windows.Forms.RichTextBox();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input comment text";
            // 
            // txtareaCommentText
            // 
            this.txtareaCommentText.Location = new System.Drawing.Point(19, 106);
            this.txtareaCommentText.Margin = new System.Windows.Forms.Padding(4);
            this.txtareaCommentText.Name = "txtareaCommentText";
            this.txtareaCommentText.Size = new System.Drawing.Size(479, 139);
            this.txtareaCommentText.TabIndex = 1;
            this.txtareaCommentText.Text = "";
            // 
            // btnAddComment
            // 
            this.btnAddComment.Location = new System.Drawing.Point(399, 265);
            this.btnAddComment.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(100, 28);
            this.btnAddComment.TabIndex = 2;
            this.btnAddComment.Text = "Add";
            this.btnAddComment.UseVisualStyleBackColor = true;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(144, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 24);
            this.comboBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Comment type";
            // 
            // AddCommentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 314);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnAddComment);
            this.Controls.Add(this.txtareaCommentText);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddCommentWindow";
            this.Text = "Add comment to Element";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtareaCommentText;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.ComboBox comboBox1;

    }
}