using System.IO;
using System.Collections.Generic;
using EA;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace EAcomments
{
    public class ExportService
    {
        private Repository Repository = null;
        private ExportWindow exportWindow = null;

        public ExportService(Repository Repository)
        {
            this.Repository = Repository;
        }

        public void exportToJSON()
        {
            List<Note> notes = new List<Note>();
            Collection collection = null;

            this.Repository.Models.Refresh();

            collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error' OR Stereotype='suggestion'", 2);

            // loop through each element and get all required information about it
            foreach (Element e in collection)
            {
                // create Note for export use
                Note note = new Note(e, Repository);
                notes.Add(note);
            }

            var JSONcontent = JsonConvert.SerializeObject(notes, Newtonsoft.Json.Formatting.Indented);

            this.exportWindow = new ExportWindow();

            if (exportWindow.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(@"" + exportWindow.FilePath, JSONcontent);
            }
        }
    }
}
