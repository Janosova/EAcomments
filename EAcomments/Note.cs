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
        [JsonProperty("positionTop")]
        public int positionTop { get; set; }
        [JsonProperty("positionRight")]
        public int positionRight { get; set; }
        [JsonProperty("positionBottom")]
        public int positionBottom { get; set; }
        [JsonProperty("positionLeft")]
        public int positionLeft { get; set; }
        [JsonProperty("relatedElements")]
        public List<RelatedElement> relatedElements { get; set; }
        [JsonProperty("tagValues")]
        public List<TagValue> tagValues { get; set; }


        //empty constructor for JSONdeserialization
        public Note() {}

        // default Note contstructor used by creating new Note in diagram via Right-click
        public Note(string stereotype, string content, Repository Repository)
        {
            this.content = content;
            this.stereotype = stereotype;

            // get current Diagram and Package 
            Diagram diagram = Repository.GetCurrentDiagram();
            Package package = Repository.GetTreeSelectedPackage();
            
            //get current selected item or selected connector
            DiagramObject selectedItem = null;
            Connector selectedConnector = null;
            try
            {
                selectedItem = diagram.SelectedObjects.GetAt(0);
            }
            catch
            {
                selectedConnector = diagram.SelectedConnector; 
            }

            // create new Note
            Element newNote;
            newNote = package.Elements.AddNew(this.stereotype, "Note");
            newNote.Stereotype = this.stereotype;
            newNote.Notes = this.content;
            newNote.Update();

            // set new Note Tagged values
            this.tagValues = new List<TagValue>();
            if(MyAddinClass.isObservedStereotype(newNote))
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
                        case "authorsName":
                            taggedValue.Value = AddCommentWindow.author;
                            break;
                        case "issueType":
                            taggedValue.Value = AddCommentWindow.issueType;
                            break;
                        case "lastModified":
                            taggedValue.Value = AddCommentWindow.lastModified;
                            break;
                    }
                    TagValue tv = new TagValue(taggedValue.Name, taggedValue.Value);
                    this.tagValues.Add(tv);
                    taggedValue.Update();
                }
            }
            newNote.Update();

            // connect node from TreeView with Diagram Element
            DiagramObject o = diagram.DiagramObjects.AddNew(this.stereotype, "Note");
            o.ElementID = newNote.ElementID;
            o.Update();

            try
            {
                // add connector between two Diagram Elements
                Connector connector = newNote.Connectors.AddNew("", "NoteLink");
                connector.SupplierID = selectedItem.ElementID;
                connector.Update();
            }
            catch
            {
                newNote.Subtype = 1;
                newNote.Update();
                Repository.Execute("UPDATE t_object SET PDATA4 = \"idref1=" + selectedConnector.ConnectorID + ";\" WHERE Object_ID = " + newNote.ElementID);
                diagram.Update();
            }

            // store other information about Note
            this.ID = newNote.ElementID;
            this.GUID = newNote.ElementGUID;
            this.flag = this.addFlag(this.stereotype);
            this.diagramGUID = diagram.DiagramGUID;
            this.diagramName = diagram.Name;
            this.packageGUID = package.PackageGUID;
            this.packageName = package.Name;
        }

        // Note constructor used by exporting
        public Note(Element e, Repository Repository)
        {
            try
            {
                // SQL query gets Collection of Elements with specified stereotype from EA.Model
                string e_id = e.ElementID.ToString();
                string diagramData = Repository.SQLQuery("SELECT t_diagram.name, t_diagram.ea_guid FROM t_diagram, t_diagramobjects WHERE t_diagramobjects.diagram_id = t_diagram.diagram_id AND t_diagramobjects.Object_ID =" + e_id);

                // get DiagramObject positions
                string positions = Repository.SQLQuery("SELECT t_diagramobjects.RectTop, t_diagramobjects.RectRight, t_diagramobjects.RectBottom, t_diagramobjects.RectLeft FROM t_diagramobjects WHERE t_diagramobjects.Object_ID =" + e.ElementID);
                this.positionTop = int.Parse(XMLParser.parseXML("RectTop", positions));
                this.positionRight = int.Parse(XMLParser.parseXML("RectRight", positions));
                this.positionBottom = int.Parse(XMLParser.parseXML("RectBottom", positions));
                this.positionLeft = int.Parse(XMLParser.parseXML("RectLeft", positions));
                // get Diagram Info
                this.diagramName = XMLParser.parseXML("name", diagramData);
                this.diagramGUID = XMLParser.parseXML("ea_guid", diagramData);
                Diagram parentDiagram = Repository.GetDiagramByGuid(this.diagramGUID);

                // get ParentElement Info (if there is any)
                Element parentElement = null;
                //string parentElementName = "";
                //string parentElementGUID = "";
                int parentID = parentDiagram.ParentID;
                if (parentID != 0)
                {
                    parentElement = Repository.GetElementByID(parentID);
                    this.parentName = parentElement.Name;
                    this.parentGUID = parentElement.ElementGUID;
                }

                // get Package Info
                int packageID = parentDiagram.PackageID;
                Package package = Repository.GetPackageByID(packageID);
                this.packageName = package.Name;
                this.packageGUID = package.PackageGUID;

                // get Connectors Info
                Collection connectors = e.Connectors;
                this.relatedElements = new List<RelatedElement>();
                foreach (Connector c in connectors)
                {
                    Element connectedElement = Repository.GetElementByID(c.SupplierID);
                    if (connectedElement.ElementGUID.Equals(e.ElementGUID))
                    {
                        connectedElement = Repository.GetElementByID(c.ClientID);
                    }
                    RelatedElement re = new RelatedElement(c.ConnectorID, connectedElement.ElementID, connectedElement.ElementGUID);
                    relatedElements.Add(re);
                }

                this.tagValues = new List<TagValue>();

                // get TaggedValues Info
                foreach (TaggedValue taggedValue in e.TaggedValues)
                {
                    TagValue tv = new TagValue(taggedValue.Name, taggedValue.Value);
                    this.tagValues.Add(tv);
                }

                this.ID = e.ElementID;
                this.GUID = e.ElementGUID;
                this.content = e.Notes;
                this.stereotype = e.Stereotype;
                this.flag = addFlag(this.stereotype);


            }
            catch (Exception) { }
            
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
                case "suggestion":
                    flag = "S";
                    break;
                default:
                    flag = "-";
                    break;
            }

            return flag;
        }
    }
}
