using System;
using EA;
using Newtonsoft.Json;

namespace EAcomments
{
    public class Note
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("GUID")]
        public string GUID { get; set; }
        [JsonProperty("flag")]
        public string flag { get; set; }
        [JsonProperty("content")]
        public string content { get; set; }
        [JsonProperty("stereotype")]
        public string stereotype { get; set; }
        [JsonProperty("parentGUID")]
        public string parentGUID { get; set; }
        [JsonProperty("parentName")]
        public string parentName { get; set; }
        [JsonProperty("diagramGUID")]
        public string diagramGUID { get; set; }
        [JsonProperty("diagramName")]
        public string diagramName { get; set; }
        [JsonProperty("packageGUID")]
        public string packageGUID { get; set; }
        [JsonProperty("packageName")]
        public string packageName { get; set; }
        [JsonProperty("connectedToID")]
        public int connectedToID { get; set; }
        [JsonProperty("connectedToGUID")]
        public string connectedToGUID { get; set; }
        [JsonProperty("connectorID")]
        public int connectorID { get; set; }

        //empty constructor for JSONdeserialization
        public Note() {}

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

        // Constructor used for Export 
        public Note(int ID, string GUID, string content, string stereotype, string diagramGUID, string diagramName, string parentGUID, string parentName, string packageGUID, string packageName, int connectorID, int connectedToID, string connectedToGUID)
        {
            this.ID = ID;
            this.GUID = GUID;
            this.content = content;
            this.stereotype = stereotype;
            this.parentGUID = parentGUID;
            this.parentName = parentName;
            this.diagramGUID = diagramGUID;
            this.diagramName = diagramName;
            this.packageGUID = packageGUID;
            this.packageName = packageName;
            this.connectorID = connectorID;
            this.connectedToID = connectedToID;
            this.connectedToGUID = connectedToGUID;
            this.flag = addFlag(this.stereotype);
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
