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
            this.commentTextLabel = new System.Windows.Forms.Label();
            this.txtAreaCommentText = new System.Windows.Forms.RichTextBox();
            this.addCommentButton = new System.Windows.Forms.Button();
            this.commentTypeBox = new System.Windows.Forms.ComboBox();
            this.commentTypeLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.authorNameLabel = new System.Windows.Forms.Label();
            this.IssueTypeLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.errorTypeBox = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.authorBox = new System.Windows.Forms.ComboBox();
            this.CardinalitiesBox = new System.Windows.Forms.GroupBox();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.targetLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.CardinalitiesBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // commentTextLabel
            // 
            this.commentTextLabel.AutoSize = true;
            this.commentTextLabel.Location = new System.Drawing.Point(9, 205);
            this.commentTextLabel.Name = "commentTextLabel";
            this.commentTextLabel.Size = new System.Drawing.Size(100, 13);
            this.commentTextLabel.TabIndex = 0;
            this.commentTextLabel.Text = "Input comment text:";
            // 
            // txtAreaCommentText
            // 
            this.txtAreaCommentText.Location = new System.Drawing.Point(12, 230);
            this.txtAreaCommentText.Name = "txtAreaCommentText";
            this.txtAreaCommentText.Size = new System.Drawing.Size(352, 114);
            this.txtAreaCommentText.TabIndex = 1;
            this.txtAreaCommentText.Text = "";
            this.txtAreaCommentText.TextChanged += new System.EventHandler(this.txtAreaCommentText_TextChanged);
            // 
            // addCommentButton
            // 
            this.addCommentButton.Location = new System.Drawing.Point(289, 364);
            this.addCommentButton.Name = "addCommentButton";
            this.addCommentButton.Size = new System.Drawing.Size(75, 23);
            this.addCommentButton.TabIndex = 2;
            this.addCommentButton.Text = "Add";
            this.addCommentButton.UseVisualStyleBackColor = true;
            this.addCommentButton.Click += new System.EventHandler(this.btnAddComment_click);
            // 
            // commentTypeBox
            // 
            this.commentTypeBox.AllowDrop = true;
            this.commentTypeBox.FormattingEnabled = true;
            this.commentTypeBox.Location = new System.Drawing.Point(101, 40);
            this.commentTypeBox.Margin = new System.Windows.Forms.Padding(2);
            this.commentTypeBox.Name = "commentTypeBox";
            this.commentTypeBox.Size = new System.Drawing.Size(200, 21);
            this.commentTypeBox.TabIndex = 3;
            this.commentTypeBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // commentTypeLabel
            // 
            this.commentTypeLabel.AutoSize = true;
            this.commentTypeLabel.Location = new System.Drawing.Point(9, 43);
            this.commentTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.commentTypeLabel.Name = "commentTypeLabel";
            this.commentTypeLabel.Size = new System.Drawing.Size(77, 13);
            this.commentTypeLabel.TabIndex = 4;
            this.commentTypeLabel.Text = "Comment type:";
            this.commentTypeLabel.Click += new System.EventHandler(this.commentTypeLabel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 5;
            // 
            // authorNameLabel
            // 
            this.authorNameLabel.AutoSize = true;
            this.authorNameLabel.Location = new System.Drawing.Point(9, 71);
            this.authorNameLabel.Name = "authorNameLabel";
            this.authorNameLabel.Size = new System.Drawing.Size(41, 13);
            this.authorNameLabel.TabIndex = 6;
            this.authorNameLabel.Text = "Author:";
            // 
            // IssueTypeLabel
            // 
            this.IssueTypeLabel.AutoSize = true;
            this.IssueTypeLabel.Location = new System.Drawing.Point(12, 14);
            this.IssueTypeLabel.Name = "IssueTypeLabel";
            this.IssueTypeLabel.Size = new System.Drawing.Size(62, 13);
            this.IssueTypeLabel.TabIndex = 8;
            this.IssueTypeLabel.Text = "Issue Type:";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(9, 102);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(33, 13);
            this.dateLabel.TabIndex = 9;
            this.dateLabel.Text = "Date:";
            // 
            // errorTypeBox
            // 
            this.errorTypeBox.FormattingEnabled = true;
            this.errorTypeBox.Location = new System.Drawing.Point(101, 11);
            this.errorTypeBox.Name = "errorTypeBox";
            this.errorTypeBox.Size = new System.Drawing.Size(200, 21);
            this.errorTypeBox.TabIndex = 10;
            this.errorTypeBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(101, 102);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // authorBox
            // 
            this.authorBox.FormattingEnabled = true;
            this.authorBox.Location = new System.Drawing.Point(101, 71);
            this.authorBox.Name = "authorBox";
            this.authorBox.Size = new System.Drawing.Size(200, 21);
            this.authorBox.TabIndex = 12;
            this.authorBox.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // CardinalitiesBox
            // 
            this.CardinalitiesBox.Controls.Add(this.targetComboBox);
            this.CardinalitiesBox.Controls.Add(this.sourceComboBox);
            this.CardinalitiesBox.Controls.Add(this.targetLabel);
            this.CardinalitiesBox.Controls.Add(this.sourceLabel);
            this.CardinalitiesBox.Enabled = false;
            this.CardinalitiesBox.Location = new System.Drawing.Point(15, 137);
            this.CardinalitiesBox.Name = "CardinalitiesBox";
            this.CardinalitiesBox.Size = new System.Drawing.Size(349, 65);
            this.CardinalitiesBox.TabIndex = 13;
            this.CardinalitiesBox.TabStop = false;
            this.CardinalitiesBox.Text = "Cardinalities";
            // 
            // targetComboBox
            // 
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Location = new System.Drawing.Point(230, 22);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(58, 21);
            this.targetComboBox.TabIndex = 3;
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(86, 22);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(58, 21);
            this.sourceComboBox.TabIndex = 2;
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(173, 25);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(51, 13);
            this.targetLabel.TabIndex = 1;
            this.targetLabel.Text = "TARGET";
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(28, 25);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(52, 13);
            this.sourceLabel.TabIndex = 0;
            this.sourceLabel.Text = "SOURCE";
            // 
            // AddCommentWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(388, 399);
            this.Controls.Add(this.CardinalitiesBox);
            this.Controls.Add(this.authorBox);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.errorTypeBox);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.IssueTypeLabel);
            this.Controls.Add(this.authorNameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.commentTypeLabel);
            this.Controls.Add(this.commentTypeBox);
            this.Controls.Add(this.addCommentButton);
            this.Controls.Add(this.txtAreaCommentText);
            this.Controls.Add(this.commentTextLabel);
            this.Name = "AddCommentWindow";
            this.Text = "Add comment to Element/Connector";
            this.Load += new System.EventHandler(this.AddCommentWindow_Load);
            this.CardinalitiesBox.ResumeLayout(false);
            this.CardinalitiesBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label commentTextLabel;
        private System.Windows.Forms.Label commentTypeLabel;
        private System.Windows.Forms.RichTextBox txtAreaCommentText;
        private System.Windows.Forms.Button addCommentButton;
        private System.Windows.Forms.ComboBox commentTypeBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label authorNameLabel;
        private System.Windows.Forms.Label IssueTypeLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.ComboBox errorTypeBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox authorBox;
        private System.Windows.Forms.GroupBox CardinalitiesBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.Label sourceLabel;
    }
}