using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;

namespace EAcomments
{
    public partial class MoreDetailsWindow : Form
    {
        private Repository Repository = null;

        // inicialize the informations about the comment
        public MoreDetailsWindow(Repository Repository, string author, string issueType, string lastModified)
        {
            InitializeComponent(); 
            this.Repository = Repository;
            this.AuthorTextBox.Text = author;
            this.IssueTypeTextBox.Text = issueType;
            this.LastModifiedTextBox.Text = lastModified;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MoreDetailsWindow_Load(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Close();
        }
    }
}
