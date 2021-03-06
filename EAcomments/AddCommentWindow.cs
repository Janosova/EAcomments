﻿using System;
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
        public static string author;
        public static string issueType;
        public static string lastModified;
        public static string sourceCardinality;
        public static string targetCardinality;

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
            string stereotype = commentTypeBox.Text;
            string content = txtAreaCommentText.Text;

            author = authorBox.Text;
            issueType = errorTypeBox.Text;
            lastModified = dateTimePicker1.Text;
            sourceCardinality = sourceComboBox.Text;
            targetCardinality = targetComboBox.Text;

            if (content != null && content != "" || sourceCardinality != null && 
                sourceCardinality != "" || targetCardinality != null && targetCardinality != "")
            {
                this.Visible = false;
                this.Close();

                // create new Note and store it
                Note note = new Note(stereotype, content, this.Repository);

                // set source and target cardinality to show in the shape script
                if (sourceCardinality != null && sourceCardinality != "" || targetCardinality != null && targetCardinality != "")
                {
                    UpdateController.assignTaggedValue(Repository, note.GUID, "sourceCardinality", sourceCardinality);
                    UpdateController.assignTaggedValue(Repository, note.GUID, "targetCardinality", targetCardinality);
                }

                // add new Note into the Comment Browser Window
                MyAddinClass.commentBrowserController.addNewElement(note);

                // refresh Diagram after new Note is added to see changes
                Diagram diagram = this.Repository.GetCurrentDiagram();
                MyAddinClass.refreshDiagram(this.Repository, diagram);
            }
        }

        // Initialization of stereotype ComboBox in Add Comment Window
        private void initComboBoxCommentType()
        {     
            commentTypeBox.Items.Add("Question");
            commentTypeBox.Items.Add("Warning");
            commentTypeBox.Items.Add("Error");
            commentTypeBox.Items.Add("Suggestion");
            commentTypeBox.SelectedIndex = 0;
        }

        // Initialization of issueType ComboBox in Add Comment Window
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
        
        // Get the names of all authors of the project
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

        // Initialization of source ComboBox in Add Comment Window
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

        // Initialization of target ComboBox in Add Comment Window
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

        private void AddCommentWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // When isuueType Cardinality is choose
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errorTypeBox.SelectedItem.Equals("Cardinalities"))
            {
                CardinalitiesBox.Enabled = true;
                commentTypeBox.Items.Clear();
                commentTypeBox.Items.Add("Question Cardinality");
                commentTypeBox.Items.Add("Warning Cardinality");
                commentTypeBox.Items.Add("Error Cardinality");
                commentTypeBox.Items.Add("Suggestion Cardinality");
                commentTypeBox.SelectedIndex = 0;
            }
            else
            {
                commentTypeBox.Items.Clear();
                initComboBoxCommentType();
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
