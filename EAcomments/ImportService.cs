using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAcomments
{
    public static class ImportService
    {
        public static ImportWindow importWindow;
        public static string JSONcontent;

        public static void ImportFromJSON()
        {
            importWindow = new ImportWindow();
            importWindow.Show();
        }

        public static void readFile()
        {
            string filePath = importWindow.filePath;
            using (StreamReader r = new StreamReader(filePath))
            {
                JSONcontent = r.ReadToEnd();
                List<Note> notes = new List<Note>();

                List<Note> no = JsonConvert.DeserializeObject<List<Note>>(JSONcontent);

                foreach(Note n in no)
                {
                    MessageBox.Show(n.content);
                }
                //NoteRepository.show();

            }
        }
    }
}
