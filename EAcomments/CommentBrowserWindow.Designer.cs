namespace EAcomments
{
    partial class CommentBrowserWindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stereotypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagramGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagramNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectedToIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectedToGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectorIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.flag,
            this.gUIDDataGridViewTextBoxColumn,
            this.contentDataGridViewTextBoxColumn,
            this.stereotypeDataGridViewTextBoxColumn,
            this.diagramGUIDDataGridViewTextBoxColumn,
            this.diagramNameDataGridViewTextBoxColumn,
            this.packageGUIDDataGridViewTextBoxColumn,
            this.packageNameDataGridViewTextBoxColumn,
            this.connectedToIDDataGridViewTextBoxColumn,
            this.connectedToGUIDDataGridViewTextBoxColumn,
            this.connectorIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.noteBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(494, 296);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // flag
            // 
            this.flag.DataPropertyName = "flag";
            this.flag.HeaderText = "Type";
            this.flag.Name = "flag";
            this.flag.ReadOnly = true;
            this.flag.Width = 50;
            // 
            // gUIDDataGridViewTextBoxColumn
            // 
            this.gUIDDataGridViewTextBoxColumn.DataPropertyName = "GUID";
            this.gUIDDataGridViewTextBoxColumn.HeaderText = "GUID";
            this.gUIDDataGridViewTextBoxColumn.Name = "gUIDDataGridViewTextBoxColumn";
            this.gUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.gUIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // contentDataGridViewTextBoxColumn
            // 
            this.contentDataGridViewTextBoxColumn.DataPropertyName = "content";
            this.contentDataGridViewTextBoxColumn.HeaderText = "Content";
            this.contentDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.contentDataGridViewTextBoxColumn.Name = "contentDataGridViewTextBoxColumn";
            this.contentDataGridViewTextBoxColumn.ReadOnly = true;
            this.contentDataGridViewTextBoxColumn.Width = 200;
            // 
            // stereotypeDataGridViewTextBoxColumn
            // 
            this.stereotypeDataGridViewTextBoxColumn.DataPropertyName = "stereotype";
            this.stereotypeDataGridViewTextBoxColumn.HeaderText = "stereotype";
            this.stereotypeDataGridViewTextBoxColumn.Name = "stereotypeDataGridViewTextBoxColumn";
            this.stereotypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.stereotypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // diagramGUIDDataGridViewTextBoxColumn
            // 
            this.diagramGUIDDataGridViewTextBoxColumn.DataPropertyName = "diagramGUID";
            this.diagramGUIDDataGridViewTextBoxColumn.HeaderText = "diagramGUID";
            this.diagramGUIDDataGridViewTextBoxColumn.Name = "diagramGUIDDataGridViewTextBoxColumn";
            this.diagramGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.diagramGUIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // diagramNameDataGridViewTextBoxColumn
            // 
            this.diagramNameDataGridViewTextBoxColumn.DataPropertyName = "diagramName";
            this.diagramNameDataGridViewTextBoxColumn.HeaderText = "Diagram";
            this.diagramNameDataGridViewTextBoxColumn.Name = "diagramNameDataGridViewTextBoxColumn";
            this.diagramNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.diagramNameDataGridViewTextBoxColumn.Width = 120;
            // 
            // packageGUIDDataGridViewTextBoxColumn
            // 
            this.packageGUIDDataGridViewTextBoxColumn.DataPropertyName = "packageGUID";
            this.packageGUIDDataGridViewTextBoxColumn.HeaderText = "packageGUID";
            this.packageGUIDDataGridViewTextBoxColumn.Name = "packageGUIDDataGridViewTextBoxColumn";
            this.packageGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.packageGUIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // packageNameDataGridViewTextBoxColumn
            // 
            this.packageNameDataGridViewTextBoxColumn.DataPropertyName = "packageName";
            this.packageNameDataGridViewTextBoxColumn.HeaderText = "Package";
            this.packageNameDataGridViewTextBoxColumn.Name = "packageNameDataGridViewTextBoxColumn";
            this.packageNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.packageNameDataGridViewTextBoxColumn.Width = 120;
            // 
            // connectedToIDDataGridViewTextBoxColumn
            // 
            this.connectedToIDDataGridViewTextBoxColumn.DataPropertyName = "connectedToID";
            this.connectedToIDDataGridViewTextBoxColumn.HeaderText = "connectedToID";
            this.connectedToIDDataGridViewTextBoxColumn.Name = "connectedToIDDataGridViewTextBoxColumn";
            this.connectedToIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.connectedToIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // connectedToGUIDDataGridViewTextBoxColumn
            // 
            this.connectedToGUIDDataGridViewTextBoxColumn.DataPropertyName = "connectedToGUID";
            this.connectedToGUIDDataGridViewTextBoxColumn.HeaderText = "connectedToGUID";
            this.connectedToGUIDDataGridViewTextBoxColumn.Name = "connectedToGUIDDataGridViewTextBoxColumn";
            this.connectedToGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.connectedToGUIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // connectorIDDataGridViewTextBoxColumn
            // 
            this.connectorIDDataGridViewTextBoxColumn.DataPropertyName = "connectorID";
            this.connectorIDDataGridViewTextBoxColumn.HeaderText = "connectorID";
            this.connectorIDDataGridViewTextBoxColumn.Name = "connectorIDDataGridViewTextBoxColumn";
            this.connectorIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.connectorIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // noteBindingSource
            // 
            this.noteBindingSource.DataSource = typeof(EAcomments.Note);
            // 
            // CommentBrowserWindow
            // 
            this.Controls.Add(this.dataGridView1);
            this.Name = "CommentBrowserWindow";
            this.Size = new System.Drawing.Size(500, 302);
            this.VisibleChanged += new System.EventHandler(this.CommentBrowserControl_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource noteBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn gUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stereotypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagramGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagramNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectedToIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectedToGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectorIDDataGridViewTextBoxColumn;
    }
}
