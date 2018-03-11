using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAcomments
{
    public partial class CommentBrowserControl : UserControl
    {
        public CommentBrowserControl()
        {
            InitializeComponent();

            this.initCols();
        }
        
        // method called when new note is being added into diagram
        public void addItem(Note note)
        {
            int rowID = dataGridView1.Rows.Add();

            DataGridViewRow row = dataGridView1.Rows[rowID];

            row.Cells["noteText"].Value = note.content;
            row.Cells["inDiagram"].Value = note.diagramName;
            row.Cells["inPackage"].Value = note.packageName;
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // initialize all collumns for browser window
        private void initCols()
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Note";
            col1.Name = "noteText";

            col2.HeaderText = "Diagram";
            col2.Name = "inDiagram";

            col3.HeaderText = "Package";
            col3.Name = "inPackage";

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3 });
        }
    }
}
