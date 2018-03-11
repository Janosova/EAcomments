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
            //get text from textarae and verify it
            string content = txtareaCommentText.Text;
            if(content != null && content != "")
            {
                this.Visible = false;

                // create new Note and store it
                Note note = new Note(content);
                note.addNote(repository);

                // add new note into the comment browser window
                if(MyAddinClass.uc_commentBrowser != null)
                {
                    MyAddinClass.uc_commentBrowser.addItem(note);
                }
                
                MyAddinClass.refreshDiagram(repository);
            }
        }

        private void initComboBox()
        {
            comboBox1.Items.Add("Question");
            comboBox1.Items.Add("Warning");
            comboBox1.Items.Add("Error");

            comboBox1.SelectedIndex = 0;
        }
    }
}
