using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAcomments
{
    public partial class ExportWindow : Form
    {
        public string FilePath { get; set; }

        public ExportWindow()
        {
            InitializeComponent();
            this.fileNameField.Text = "Comments";
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            saveFileBrowser.FileName = this.fileNameField.Text;
            saveFileBrowser.Filter = "JSON (*.json) | *.json*"; 
            saveFileBrowser.ShowDialog();
        }

        private void saveFileBrowser_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFileBrowser.FileName);
            this.fileNameField.Text = fileName;
            this.filePathField.Text = saveFileBrowser.FileName;
        }

        private void FileFieldsChanged(object sender, EventArgs e)
        {
            exportButton.Enabled = !string.IsNullOrWhiteSpace(this.filePathField.Text) 
                                    && !string.IsNullOrWhiteSpace(this.fileNameField.Text);

            this.FilePath = this.filePathField.Text + ".json";
        }
    }
}
