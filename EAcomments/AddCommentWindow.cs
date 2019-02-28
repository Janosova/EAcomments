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
            this.initComboBoxCommentType();
            this.initComboBoxErrorType();
            this.authorName();
        }

        private void btnAddComment_click(object sender, EventArgs e)
        {
            //get text from Textarea and verify it
            string content = txtAreaCommentText.Text;
            string stereotype = commentTypeBox.Text;

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
        private void initComboBoxCommentType()
        {
            commentTypeBox.Items.Add("question");
            commentTypeBox.Items.Add("warning");
            commentTypeBox.Items.Add("error");
            commentTypeBox.Items.Add("suggestion");
            commentTypeBox.SelectedIndex = 0;
        }

        private void initComboBoxErrorType() {
            errorTypeBox.Items.Add("Wrong description");
            errorTypeBox.Items.Add("Wrong orientation");
            errorTypeBox.Items.Add("Missing connector");
            errorTypeBox.Items.Add("Wrong type of connector");
            errorTypeBox.Items.Add("Wrong cardinalities");
            errorTypeBox.Items.Add("Other");
            errorTypeBox.SelectedIndex = 0;

        }
        
        private void authorName() {
            Collection authorCollection = Repository.GetElementSet("SELECT * FROM t_object", 2);
            IList<String> authors = new List<String>();
            foreach (Element e in authorCollection) {
                authors.Add(e.Author);
            }
            authorBox.Items.AddRange(authors.Distinct().ToArray());
            authorBox.SelectedIndex = 0;
        }

        //tieto triedy neviem preco sa mi vytvorili zatial zistit
        private void AddCommentWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
