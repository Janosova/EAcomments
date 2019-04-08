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
    public class ImportService
    {
        private Repository Repository = null;
        private ImportWindow importWindow = null;

        public ImportService(Repository Repository)
        {
            this.Repository = Repository;
            MyAddinClass.commentBrowserController = new CommentBrowserController(this.Repository);
            this.importWindow = new ImportWindow();
        }

        public void ImportFromJSON()
        {
            if (this.importWindow.ShowDialog() == DialogResult.OK)
            {
                string filePath = this.importWindow.FilePath;
                using (StreamReader r = new StreamReader(filePath))
                {
                    // Read file and deserialize from JSON to Note type array
                    var JSONcontent = r.ReadToEnd();
                    List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(JSONcontent);

                    // Loop through each imported Note and 
                    foreach (Note n in notes)
                    {
                        this.importNoteToModel(n);
                    }
                }
                MyAddinClass.commentBrowserController.refreshWindow();
            }
        }

        private void importNoteToModel(Note n)
        {
            Boolean exist = false;
            Diagram diagram = null;
            Package package = null;
            Element parentElement = null;
            Collection collection = null;

            try
            {
                package = this.Repository.GetPackageByGuid(n.packageGUID);
                diagram = this.Repository.GetDiagramByGuid(n.diagramGUID);
                parentElement = this.Repository.GetElementByGuid(n.parentGUID);

                collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='" + n.stereotype + "' ", 2);
                string noteOriginGUID = null;
                string noteState = null;
                string noteAuthorsName = "";
                string noteIssueType = "";
                string noteLastModified = "";
                string noteSourceCardinality = "";
                string noteTargetCardinality = "";
                foreach (TagValue tv in n.tagValues)
                {
                    if (tv.name.Equals("origin"))
                    {
                        noteOriginGUID = tv.value;
                    }
                    switch (tv.name)
                    {
                        case "origin":
                            noteOriginGUID = tv.value;
                            break;
                        case "state":
                            noteState = tv.value;
                            break;
                        case "authorsName":
                            noteAuthorsName = tv.value;
                            break;
                        case "issueType":
                            noteIssueType = tv.value;
                            break;
                        case "lastModified":
                            noteLastModified = tv.value;
                            break;
                        case "sourceCardinality":
                            noteSourceCardinality = tv.value;
                            break;
                        case "targetCardinality":
                            noteTargetCardinality = tv.value;
                            break;
                        default:
                            break;
                    }
                }
                
                // Loop through all Elements in Model and check if Note already exists in Model
                foreach (Element e in collection)
                {
                    string elementOriginGUID = null;
                    foreach (TaggedValue tv in e.TaggedValues)
                    {
                        if (tv.Name.Equals("origin"))
                        {
                            elementOriginGUID = tv.Value;
                        }
                    }
                    if (elementOriginGUID == noteOriginGUID)
                    {
                        exist = true;
                        break;
                    }
                }
                // if Element is not in the Model, add it
                if (!exist)
                {
                    Element note = package.Elements.AddNew(n.stereotype, "Note");
                    note.Stereotype = n.stereotype;
                    note.Notes = n.content;
                    note.Update();

                    foreach (TaggedValue taggedValue in note.TaggedValues)
                    {
                        switch (taggedValue.Name)
                        {
                            case "origin":
                                taggedValue.Value = noteOriginGUID;
                                break;
                            case "state":
                                taggedValue.Value = noteState;
                                break;
                            case "authorsName":
                                taggedValue.Value = noteAuthorsName;
                                break;
                            case "issueType":
                                taggedValue.Value = noteIssueType;
                                break;
                            case "lastModified":
                                taggedValue.Value = noteLastModified;
                                break;
                            case "sourceCardinality":
                                taggedValue.Value = noteSourceCardinality;
                                break;
                            case "targetCardinality":
                                taggedValue.Value = noteTargetCardinality;
                                break;
                            default:
                                break;
                        }
                        taggedValue.Update();
                    }
                    note.Update();

                    // Create new DiagramObject and link it with Element
                    DiagramObject o = diagram.DiagramObjects.AddNew(n.stereotype, "Note");
                    o.ElementID = note.ElementID;

                    // Set DiagramObject positions
                    o.top = n.positionTop;
                    o.right = n.positionRight;
                    o.bottom = n.positionBottom;
                    o.left = n.positionLeft;

                    o.Update();
                    int connectors = 0;
                    // Generate all Connectors
                    foreach (RelatedElement relatedElement in n.relatedElements)
                    {
                        // Generate connectors Note to element
                        if (!relatedElement.connectorID.Equals(-1))
                        {
                            for (short i = 0; i < diagram.DiagramObjects.Count; i++)
                            {
                                DiagramObject diagramObject = diagram.DiagramObjects.GetAt(i);
                                if (diagramObject.ElementID == relatedElement.connectedToID)
                                {
                                    connectors++;
                                }
                            }
                            if (connectors > 0)
                            {
                                Connector connector = note.Connectors.AddNew("", "NoteLink");
                                connector.SupplierID = relatedElement.connectedToID;
                                connector.Update();
                            }
                        }
                        // Generate connector Note to connector
                        else
                        {
                            note.Subtype = 1;
                            note.Update();
                            Repository.Execute("UPDATE t_object SET PDATA4 = \"idref1=" + relatedElement.connectedToID + ";\" WHERE Object_ID = " + note.ElementID);
                            diagram.Update();
                        }
                    }
                }
                MyAddinClass.refreshDiagram(Repository, diagram);
            }
            catch
            {
                MessageBox.Show("Cant import note " + n.content + ". Parent Diagram is missing.");
            }
        }
    }
}
