﻿using System;
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
                // Read file and deserialize from JSON to Note type array
                JSONcontent = r.ReadToEnd();
                List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(JSONcontent);

                // Loop through each imported Note and 
                foreach(Note n in notes)
                {
                    importNoteToModel(n);
                }
            }
            CommentBrowserController.refreshWindow();

        }

        private static void importNoteToModel(Note n)
        {
            Boolean exist = false;
            Diagram diagram = null;
            Package package = null;
            Element parentElement = null;

            try
            {
                package = Repository.GetPackageByGuid(n.packageGUID);
                diagram = Repository.GetDiagramByGuid(n.diagramGUID);
                parentElement = Repository.GetElementByGuid(n.parentGUID);

                Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='" + n.stereotype + "' ", 2);
                string noteOriginGUID = null;
                string noteState = null;
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
