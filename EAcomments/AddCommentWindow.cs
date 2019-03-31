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
        public static string authorsName;
        public static string issueType;
        public static string lastModified;

        public AddCommentWindow(Repository Repository)
        {
            InitializeComponent();
            this.Repository = Repository;
            this.initComboBoxCommentType();
            this.initComboBoxErrorType();
            this.authorName();
            this.initSource();
            this.initTarget();
        }

        //when button add comment is clicked
        private void btnAddComment_click(object sender, EventArgs e)
        {
            //get text from Textarea and verify it
            string content = txtAreaCommentText.Text;
            string stereotype = commentTypeBox.Text;

            //get authors name, issue type and date of creating the note
            //authorsName = authorBox.Text;
            //issueType = errorTypeBox.Text;
            //lastModified = dateTimePicker1.Text;
            string author = authorBox.Text;
            string issueType = errorTypeBox.Text;
            string lastModified = dateTimePicker1.Text;

            if(content != null && content != "")
            {
                this.Visible = false;
                this.Close();

                // create new Note and store it
                Note note = new Note(stereotype, content, author, issueType, lastModified, this.Repository);

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

        private void initComboBoxErrorType()
        {
            errorTypeBox.Items.Add("Description");
            errorTypeBox.Items.Add("Orientation");
            errorTypeBox.Items.Add("Missing connector");
            errorTypeBox.Items.Add("Type of connector");
            errorTypeBox.Items.Add("Cardinalities");
            errorTypeBox.Items.Add("Other");
            errorTypeBox.SelectedIndex = 0;

        }
        
        private void authorName()
        {
            Collection authorCollection = Repository.GetElementSet("SELECT * FROM t_object", 2);
            IList<String> authors = new List<String>();
            foreach (Element e in authorCollection) {
                authors.Add(e.Author);
            }
            authorBox.Items.AddRange(authors.Distinct().ToArray());
            authorBox.SelectedIndex = 0;
        }

        private void initSource()
        {
            sourceComboBox.Items.Add("*");
            sourceComboBox.Items.Add("0");
            sourceComboBox.Items.Add("0..*");
            sourceComboBox.Items.Add("0..1");
            sourceComboBox.Items.Add("1");
            sourceComboBox.Items.Add("1..");
            sourceComboBox.Items.Add("1..*");
        }

        private void initTarget()
        {
            targetComboBox.Items.Add("*");
            targetComboBox.Items.Add("0");
            targetComboBox.Items.Add("0..*");
            targetComboBox.Items.Add("0..1");
            targetComboBox.Items.Add("1");
            targetComboBox.Items.Add("1..");
            targetComboBox.Items.Add("1..*");
        }

        //tieto triedy neviem preco sa mi vytvorili zatial zistit
        private void AddCommentWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //issue type combo box
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errorTypeBox.SelectedItem.Equals("Cardinalities"))
            {
                CardinalitiesBox.Enabled = true;

            }
            else
            {
                sourceComboBox.ResetText();
                targetComboBox.ResetText();
                CardinalitiesBox.Enabled = false;
            }
            
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

        private void commentTypeLabel_Click(object sender, EventArgs e)
        {

        }

        private void txtAreaCommentText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
