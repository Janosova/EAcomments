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
        BindingSource bindingSourse;

        public CommentBrowserControl()
        {
            InitializeComponent();
            this.bindingSourse = new BindingSource();
            
            this.initCols();
        }
        
        // method called when new note is being added into diagram
        public void addItem(Note note)
        {
            bindingSourse.Add(note);
            dataGridView1.DataSource = bindingSourse;
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
    }
}
