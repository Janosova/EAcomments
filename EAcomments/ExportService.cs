using System.IO;
using System.Collections.Generic;
using EA;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace EAcomments
{
    public static class ExportService
    {
        public static ExportWindow exportWindow;
        public static string JSONcontent;

        public static void exportToJson(Repository Repository)
        {
            List<Note> notes = new List<Note>();

            Repository.Models.Refresh();

            Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error'", 2);

            // loop through each element and get all required information about it
            foreach (Element e in collection)
            {
                // create Note for export uses
                Note note = new Note(e, Repository);
                notes.Add(note);
            }
            JSONcontent = JsonConvert.SerializeObject(notes, Newtonsoft.Json.Formatting.Indented);

            exportWindow = new ExportWindow();
            exportWindow.Show();
        }

        public static void writeFile()
        {
            // store JSONcontent into specified file and path
            string filePath = exportWindow.filePath;
            System.IO.File.WriteAllText(@"" + filePath, JSONcontent);
        }
    }
}
