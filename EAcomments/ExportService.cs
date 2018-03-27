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

            Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error'", 2);

            // loop through each element and get all required information about it
            foreach (Element e in collection)
            {
                // SQL query gets Collection of Elements with specified stereotype from EA.Model
                string e_id = e.ElementID.ToString();
                string diagramData = Repository.SQLQuery("SELECT t_diagram.name, t_diagram.ea_guid FROM t_diagram, t_diagramobjects WHERE t_diagramobjects.diagram_id = t_diagram.diagram_id AND t_diagramobjects.Object_ID =" + e_id);

                // get diagram Info
                string diagramName = XMLParser.parseXML("name", diagramData);
                string diagramGUID = XMLParser.parseXML("ea_guid", diagramData);
                Diagram parentDiagram = Repository.GetDiagramByGuid(diagramGUID);

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
                    connectorID = c.ConnectorID;
                    // get Info about connecter element to note
                    supplierID = c.SupplierID;
                    connectedElement = Repository.GetElementByID(supplierID);
                    connectedToID = connectedElement.ElementID;
                    connectedToGUID = connectedElement.ElementGUID;
                }

                // create new note from gathered Info
                Note note = new Note(e.ElementID, e.ElementGUID, e.Notes, e.Stereotype, diagramGUID, diagramName, parentElementGUID, parentElementName, packageGUID, packageName, connectorID, connectedToID, connectedToGUID);
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
            MessageBox.Show("You have closed the form");
        }
    }
}
