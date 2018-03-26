using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EA;

namespace EAcomments
{
    public class Note
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string flag { get; set; }
        public string content { get; set; }
        public string stereotype { get; set; }
        public string parentGUID { get; set; }
        public string parentName { get; set; }
        public string diagramGUID { get; set; }
        public string diagramName { get; set; }
        public string packageGUID { get; set; }
        public string packageName { get; set; }
        public int connectedToID { get; set; }
        public string connectedToGUID { get; set; }
        public int connectorID { get; set; }

        public Note(string content, string stereotype)
        {
            this.content = content;
            this.stereotype = stereotype;
        }

        public Note(int ID, string GUID, string flag, string content, string stereotype, string diagramGUID, string diagramName, string packageGUID, string packageName)
        {
            this.ID = ID;
            this.GUID = GUID;
            this.flag = flag;
            this.content = content;
            this.stereotype = stereotype;
            this.diagramGUID = diagramGUID;
            this.diagramName = diagramName;
            this.packageGUID = packageGUID;
            this.packageName = packageName;
        }
        //export
        public Note(int ID, string GUID, string content, string stereotype, string diagramGUID, string diagramName, string parentGUID, string parentName, string packageGUID, string packageName)
        {
            this.ID = ID;
            this.GUID = GUID;
            this.flag = flag;
            this.content = content;
            this.stereotype = stereotype;
            this.diagramGUID = diagramGUID;
            this.diagramName = diagramName;
            this.packageGUID = packageGUID;
            this.packageName = packageName;
        }

        public void addNote(Repository Repository)
        {
            // get current Diagram, Package and selected Item
            Diagram diagram = Repository.GetCurrentDiagram();
            Package package = Repository.GetTreeSelectedPackage();
            DiagramObject selectedItem = diagram.SelectedObjects.GetAt(0);

            // create new node
            Element newNote;
            newNote = package.Elements.AddNew(this.stereotype, "Note");
            newNote.Stereotype = this.stereotype;
            newNote.Notes = this.content;
            newNote.Update();

            // connect node from TreeView with Diagram Element
            DiagramObject o = diagram.DiagramObjects.AddNew(this.stereotype, "Note");
            o.ElementID = newNote.ElementID;
            o.Update();

            // add connector between two Diagram Elements
            Connector connector = newNote.Connectors.AddNew("", "NoteLink");
            connector.SupplierID = selectedItem.ElementID;
            connector.Update();

            // store other information about Note
            this.ID = newNote.ElementID;
            this.GUID = newNote.ElementGUID;
            this.flag = this.addFlag(this.stereotype);
            this.diagramGUID = diagram.DiagramGUID;
            this.diagramName = diagram.Name;
            this.packageGUID = package.PackageGUID;
            this.packageName = package.Name;
            this.connectorID = connector.ConnectorID;
            this.connectedToID = selectedItem.ElementID;
            this.connectedToGUID = selectedItem.InstanceGUID;
        }

        private string addFlag(string type)
        {
            string flag = "";

            switch (type)
            {
                case "question":
                    flag = "Q";
                    break;
                case "warning":
                    flag = "W";
                    break;
                case "error":
                    flag = "E";
                    break;
                default:
                    flag = "-";
                    break;
            }

            return flag;
        }
    }
}
