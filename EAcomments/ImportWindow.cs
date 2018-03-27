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
        public string filePath { get; set; }
        public ImportWindow()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileBrowser.ShowDialog();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            // verify if file exists and if extesion is .json
            if((File.Exists(this.filePathField.Text)) && (Path.GetExtension(this.filePathField.Text).ToLower() == ".json"))
            {
                filePath = this.filePathField.Text;
                //Close Window
                this.Visible = false;
                this.Close();
            }
        }

        private void openFileBrowser_FileOk(object sender, CancelEventArgs e)
        {
            this.filePathField.Text = openFileBrowser.FileName;
        }

        private void ImportWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            ImportService.readFile();
        }
    }
}
