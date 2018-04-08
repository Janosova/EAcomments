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
        public DataGridView dataGridView { get; set; }
        public Repository Repository { get; set; }

        public CommentBrowserWindow()
        {
            InitializeComponent();
            this.bindingSourse = new BindingSource();
            this.dataGridView = dataGridView1;
            this.state.TrueValue = true;
            this.state.FalseValue = false;
        }

        public void clearWindow()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();
        }

        public void initExistingNotes(List<Note> notes, Repository Repository)
        {
            this.Repository = Repository;
            foreach (Note n in notes)
            {
                addItem(n);
            }
            initCheckBoxes();
        }

        private void initCheckBoxes()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Note n = (Note)row.DataBoundItem;
                foreach(TagValue tv in n.tagValues)
                {
                    if(tv.name.Equals("state"))
                    {
                        if(tv.value.Equals("resolved"))
                        {
                            DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)row.Cells[9];
                            checkbox.TrueValue = true;
                            checkbox.Value = checkbox.TrueValue;
                        }
                    }
                }
            }
        }

        // method called when new note is being added into diagram
        public void addItem(Note note)
        {
            this.bindingSourse.Add(note);
            dataGridView1.DataSource = this.bindingSourse;
        }
        
        public void updateContent(string lastElementGUID, string currentElementGUID, string updatedContent)
        {
            int i = 0;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                Note n = (Note)row.DataBoundItem;
                // update Row in Comment Browser Window
                if(n.GUID.Equals(lastElementGUID))
                {
                    n.content = updatedContent;
                }
                // select Row in Comment Browser Window
                else if (n.GUID.Equals(currentElementGUID))
                {
                    dataGridView1.CurrentRow.Selected = true;
                    dataGridView1.Rows[i].Selected = true;
                }
                i++;
            }
            dataGridView1.Refresh();
            dataGridView1.Update();
        }

        public void deleteElement(string elementGUID)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Note n = (Note)row.DataBoundItem;
                if (n.GUID.Equals(elementGUID))
                {
                    dataGridView1.Rows.Remove(row);
                    break;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CommentBrowserController.updateElementState(e, dataGridView1);
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            UpdateController.sync(this.Repository);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            ImportService.ImportFromJSON(this.Repository);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            ExportService.exportToJson(this.Repository);
        }
    }
}
