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
    public partial class AddCommentWindow : Form
    {
        private Repository Repository = null;

        public AddCommentWindow(Repository Repository)
        {
            InitializeComponent();
            this.Repository = Repository;
            this.initComboBox();
        }

        private void btnAddComment_click(object sender, EventArgs e)
        {
            //get text from Textarea and verify it
            string content = txtareaCommentText.Text;
            string stereotype = comboBox1.Text;

            if(content != null && content != "")
            {
                this.Visible = false;
                this.Close();

                // create new Note and store it
                Note note = new Note(stereotype, content, this.Repository);

                // add new Note into the Comment Browser Window
                MyAddinClass.commentBrowserController.addNewElement(note);

                // refresh Diagram after new Note is added to see changes
                Diagram diagram = this.Repository.GetCurrentDiagram();
                MyAddinClass.refreshDiagram(this.Repository, diagram);
            }
        }

        // Initialization of ComboBox in Add Comment Window
        private void initComboBox()
        {
            comboBox1.Items.Add("question");
            comboBox1.Items.Add("warning");
            comboBox1.Items.Add("error");

            comboBox1.SelectedIndex = 0;
        }
    }
}
