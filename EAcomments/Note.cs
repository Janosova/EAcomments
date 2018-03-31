using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        [JsonProperty("tagValues")]
        public List<TagValue> tagValues { get; set; }


        //empty constructor for JSONdeserialization
        public Note() {}
/*
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
*/
        // default Note contstructor used by creating new Note in diagram
        public Note(string stereotype, string content, Repository Repository)
        {

            this.content = content;
            this.stereotype = stereotype;

            // get current Diagram, Package and selected Item
            Diagram diagram = Repository.GetCurrentDiagram();
            Package package = Repository.GetTreeSelectedPackage();
            DiagramObject selectedItem = diagram.SelectedObjects.GetAt(0);

            // create new Note
            Element newNote;
            newNote = package.Elements.AddNew(this.stereotype, "Note");
            newNote.Stereotype = this.stereotype;
            newNote.Notes = this.content;
            newNote.Update();

            // set new Note Tagged values
            this.tagValues = new List<TagValue>();
            if (newNote.Stereotype == "question")
            {
                foreach (TaggedValue taggedValue in newNote.TaggedValues)
                {
                    switch (taggedValue.Name)
                    {
                        default:
                            break;
                        case "origin":
                            taggedValue.Value = newNote.ElementGUID;
                            break;
                        case "state":
                            taggedValue.Value = "unresolved";
                            break;
                    }
                    TagValue tv = new TagValue(taggedValue.Name, taggedValue.Value);
                    this.tagValues.Add(tv);
                    MessageBox.Show("Vytvoril s taggedValue " + taggedValue.Name + " s hodnotou " + taggedValue.Value);
                    taggedValue.Update();
                }
            }
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

        // Note constructor used by exporting
        public Note(Element e, Repository Repository)
        {
            // SQL query gets Collection of Elements with specified stereotype from EA.Model
            string e_id = e.ElementID.ToString();
            string diagramData = Repository.SQLQuery("SELECT t_diagram.name, t_diagram.ea_guid FROM t_diagram, t_diagramobjects WHERE t_diagramobjects.diagram_id = t_diagram.diagram_id AND t_diagramobjects.Object_ID =" + e_id);
            
            // get diagram Info
            string diagramName = XMLParser.parseXML("name", diagramData);
            string diagramGUID = XMLParser.parseXML("ea_guid", diagramData);
            Diagram parentDiagram = null;
            parentDiagram = Repository.GetDiagramByGuid(diagramGUID);

            // get parent Info
            int parentID = parentDiagram.ParentID;
            Element parentElement = null;
            string parentElementName = "";
            string parentElementGUID = "";
            if (parentID != 0)
            {
                parentElement = Repository.GetElementByID(parentID);
                parentElementName = parentElement.Name;
                parentElementGUID = parentElement.ElementGUID;
            }

            // get package Info
            int packageID = parentDiagram.PackageID;
            Package package = Repository.GetPackageByID(packageID);
            string packageName = package.Name;
            string packageGUID = package.PackageGUID;

            // get connector Info
            int connectorID = 0;
            int supplierID = 0;
            Element connectedElement = null;
            int connectedToID = 0;
            string connectedToGUID = null;
            Collection connectors = e.Connectors;
            foreach (Connector c in connectors)
            {
                // get Info about Connector element to note
                connectorID = c.ConnectorID;
                supplierID = c.SupplierID;
                connectedElement = Repository.GetElementByID(supplierID);
                connectedElement = Repository.GetElementByID(supplierID);
                connectedToID = connectedElement.ElementID;
                connectedToGUID = connectedElement.ElementGUID;
            }

            this.tagValues = new List<TagValue>();

            foreach (TaggedValue taggedValue in e.TaggedValues)
            {
                TagValue tv = new TagValue(taggedValue.Name, taggedValue.Value);
                this.tagValues.Add(tv);
            }

            this.ID = e.ElementID;
            this.GUID = e.ElementGUID;
            this.content = e.Notes;
            this.stereotype = e.Stereotype;
            this.parentGUID = parentElementGUID;
            this.parentName = parentElementName;
            this.diagramGUID = diagramGUID;
            this.diagramName = diagramName;
            this.packageGUID = packageGUID;
            this.packageName = packageName;
            this.connectorID = connectorID;
            this.connectedToID = connectedToID;
            this.connectedToGUID = connectedToGUID;
            this.flag = addFlag(this.stereotype);
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
