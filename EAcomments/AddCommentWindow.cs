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
            //get text from textarea and verify it
            string content = txtareaCommentText.Text;
            string stereotype = comboBox1.Text;

            if(content != null && content != "")
            {
                this.Visible = false;
                this.Close();

                // create new Note and store it
                Note note = new Note(content, stereotype);
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
            comboBox1.Items.Add("question");
            comboBox1.Items.Add("warning");
            comboBox1.Items.Add("error");

            comboBox1.SelectedIndex = 0;
        }
    }
}
