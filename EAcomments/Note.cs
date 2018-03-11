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
        public string type { get; set; }
        public string content { get; set; }
        public string diagramGUID { get; set; }
        public string diagramName { get; set; }
        public string packageGUID { get; set; }
        public string packageName { get; set; }
        public int connectedToID { get; set; }
        public string connectedToGUID { get; set; }
        public int connectorID { get; set; }

        public Note(string content)
        {
            this.content = content;
        }

        public void addNote(Repository Repository)
        {
            // get current Diagram, Package and selected Item
            Diagram diagram = Repository.GetCurrentDiagram();
            Package package = Repository.GetTreeSelectedPackage();
            DiagramObject selectedItem = diagram.SelectedObjects.GetAt(0);

            // create new node
            Element newNote;
            newNote = package.Elements.AddNew(this.type, "Note");
            newNote.Notes = this.content;
            newNote.SetAppearance(1, 0, 15960070);
            newNote.Update();

            // connect node from TreeView with Diagram Element
            DiagramObject o = diagram.DiagramObjects.AddNew(this.type, "Note");
            o.ElementID = newNote.ElementID;
            o.Update();

            // add connector between two Diagram Elements
            Connector connector = newNote.Connectors.AddNew("", "Note-link");
            connector.SupplierID = selectedItem.ElementID;
            connector.Update();

            // store other information about Note
            this.ID = o.InstanceID;
            this.GUID = o.InstanceGUID;
            this.diagramGUID = diagram.DiagramGUID;
            this.diagramName = diagram.Name;
            this.packageGUID = package.PackageGUID;
            this.packageName = package.Name;
            this.connectorID = connector.ConnectorID;
            this.connectedToID = selectedItem.ElementID;
            this.connectedToGUID = selectedItem.InstanceGUID;
        }
    }
}
