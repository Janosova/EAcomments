using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;

namespace EAcomments
{
    public partial class CommentBrowserWindow : UserControl
    {
        BindingSource bindingSourse;

        public CommentBrowserWindow()
        {
            InitializeComponent();
            this.bindingSourse = new BindingSource();
        }

        public void initExistingNotes(List<Note> notes)
        {
            foreach(Note n in notes)
            {
                addItem(n);
            }
        }


        // method called when new note is being added into diagram
        public void addItem(Note note)
        {
            bindingSourse.Add(note);
            dataGridView1.DataSource = bindingSourse;
        }
        
        public void updateItem(string GUID, string updatedContent)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                Note n = (Note)row.DataBoundItem;
                if(n.GUID.Equals(GUID))
                {
                    n.content = updatedContent;
                    MessageBox.Show("Porovnavam  " + GUID + " s window object GUID" + n.GUID + " ak ano, upravujem " + n.content + " na " + updatedContent);
                }
            }

            dataGridView1.Refresh();
            dataGridView1.Update();


        }

        // initialize all collumns for browser window
        private void initCols()
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Type";
            col1.Name = "noteType";

            col2.HeaderText = "Note";
            col2.Name = "noteText";

            col3.HeaderText = "Diagram";
            col3.Name = "inDiagram";

            col4.HeaderText = "Package";
            col4.Name = "inPackage";

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4 });
        }

        // Handles events when clicked on specified Row
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Note n = (Note)dataGridView1.CurrentRow.DataBoundItem;
            CommentBrowserController.openDiagramWithGUID(n.diagramGUID);
        }

        private void CommentBrowserControl_VisibleChanged(object sender, EventArgs e)
        {
            if(!this.Visible)
            {
                CommentBrowserController.windowClosed();
            }
        }
    }
}
