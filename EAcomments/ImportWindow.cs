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
    public partial class ImportWindow : Form
    {
        public string JSONcontent { get; set; }
        public string FilePath { get; set; }
        public ImportWindow()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileBrowser.ShowDialog();
        }

        private void openFileBrowser_FileOk(object sender, CancelEventArgs e)
        {
            this.filePathField.Text = openFileBrowser.FileName;
        }

        private void fileFieldsChanged(object sender, EventArgs e)
        {
            importButton.Enabled = !string.IsNullOrWhiteSpace(this.filePathField.Text);

            this.FilePath = this.filePathField.Text;
        }
    }
}
