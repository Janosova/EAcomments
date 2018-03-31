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
        private Repository repository;
        public AddCommentWindow(Repository Repository)
        {
            InitializeComponent();
            this.repository = Repository;
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
                Note note = new Note(stereotype, content, repository);
                
                // add new Note into the comment browser window
                if(CommentBrowserController.uc_commentBrowser != null)
                {
                    CommentBrowserController.uc_commentBrowser.addItem(note);
                }
                Diagram diagram = repository.GetCurrentDiagram();
                MyAddinClass.refreshDiagram(repository, diagram);
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
