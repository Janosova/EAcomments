using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;

namespace EAcomments
{
    public static class ImportService
    {
        public static ImportWindow importWindow;
        public static string JSONcontent;
        public static Repository Repository;

        public static void ImportFromJSON(Repository repository)
        {
            Repository = repository;
            importWindow = new ImportWindow();
            importWindow.Show();
        }

        public static void readFile()
        {
            string filePath = importWindow.filePath;
            using (StreamReader r = new StreamReader(filePath))
            {
                // read file and deserialize from JSON to Note type array
                JSONcontent = r.ReadToEnd();
                List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(JSONcontent);

                // loop through each imported Note and 
                foreach(Note n in notes)
                {
                    importNoteToModel(n);
                }
            }
        }

        private static void importNoteToModel(Note n)
        {
            Boolean exist = false;
            Diagram diagram = Repository.GetDiagramByGuid(n.diagramGUID);
            Package package = Repository.GetPackageByGuid(n.packageGUID);
            Element parentElement = Repository.GetElementByGuid(n.parentGUID);
            Element connectedToElement = Repository.GetElementByGuid(n.connectedToGUID);
            
            Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='" + n.stereotype + "' ", 2);


            foreach(Element e in collection)
            {
                if(e.ElementGUID == n.GUID)
                {
                    exist = true;
                    MessageBox.Show("Element " + n.content + " is already in MODEL");
                    break;
                }
            }
            if(!exist)
            {
                Element note = package.Elements.AddNew(n.stereotype, "Note");
                note.Stereotype = n.stereotype;
                note.Notes = n.content;
                note.Update();

                DiagramObject o = diagram.DiagramObjects.AddNew(n.stereotype, "Note");
                o.ElementID = note.ElementID;
                o.Update();

                Connector connector = note.Connectors.AddNew("", "NoteLink");
                connector.SupplierID = connectedToElement.ElementID;
                connector.Update();

                MyAddinClass.refreshDiagram(Repository, diagram);
                MessageBox.Show("Note Imported");
            }
            
        }
    }
}
