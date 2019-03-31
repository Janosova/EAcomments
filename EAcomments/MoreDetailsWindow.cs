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
        public MoreDetailsWindow(Repository Repository, String author)
        {
            InitializeComponent();
            this.Repository = Repository;
            this.AuthorTextBox.Text = author;
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
