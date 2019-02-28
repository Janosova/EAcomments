using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using EA;

// Enterprise Architect simple team review Add-in 
// using Comments to point out questions, warning, errors

namespace EAcomments
{
    public class MyAddinClass
    {
        // Define menu constants
        const string menuHeader = "-&Comment Plugin";

        // Define Main-menu on EA panel
        const string menuShowCommentWindow = "&Show Comment Browser";
        const string menuImportComments = "&Import Comments";
        const string menuExportComments = "&Export Comments";

        // Define Sub-menu for Diagram Location
        const string menuAddCommentToElement = "&Add comment to Element";
        const string changeStateToResolved = "&Resolve Comment";
        const string changeStateToUnresolved = "&Unresolve Comment";

        // Controllers
        public static CommentBrowserController commentBrowserController = null;

        // Services
        public static ImportService importService = null;
        public static ExportService exportService = null;

        // Windows
        AddCommentWindow addCommentWindow = null;

        // Define Sub-menu for Treeview Location
        // -- nothing yet -- 

        // Method connects to EA Repository each time EA opens
        public String EA_Connect(EA.Repository Repository)
        {
            commentBrowserController = new CommentBrowserController(Repository);
            importService = new ImportService(Repository);
            exportService = new ExportService(Repository);
            return string.Empty;
        }

        public virtual Object EA_OnInitializeTechnologies(Repository Repository)
        {
            string technology = "";

            Assembly assem = this.GetType().Assembly;
            // EAcomments.Properties.Resources
            using (Stream stream = assem.GetManifestResourceStream("EAcomments.xml"))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        technology = reader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Initializing Technology");
                }

            }

            return technology;
        }

        // Define default Menu settings
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            if (MenuName == "")
                return menuHeader;

            // generate submenus according to location where user clicks
            switch (Location)
            {
                case "Diagram":
                    if (MenuName == menuHeader)
                        return new string[] { menuAddCommentToElement, changeStateToResolved, changeStateToUnresolved };
                    break;
                case "MainMenu":
                    if (MenuName == menuHeader)
                        return new string[] { menuShowCommentWindow, menuImportComments, menuExportComments };
                    break;
                case "TreeView":
                    if (MenuName == menuHeader)
                        return new string[] { /* -- nothing yet -- */ };
                    break;
            }
            return string.Empty;
        }

        // Method verifies if Model is opened
        bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                Collection c = Repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Method called when menu is being initialized or user tries to use it
        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    // Main menu
                    case menuShowCommentWindow:
                        IsEnabled = true;
                        break;
                    case menuImportComments:
                        IsEnabled = true;
                        break;
                    case menuExportComments:
                        IsEnabled = true;
                        break;

                    // Diagram menu
                    case menuAddCommentToElement:
                        if (IsElementSelected(Repository)) { IsEnabled = true; }
                        else { IsEnabled = false; }
                        break;
                    case changeStateToResolved:
                        if (getAndCheckElement(Repository)) { IsEnabled = true; }
                        else { IsEnabled = false; }
                        break;
                    case changeStateToUnresolved:
                        if (getAndCheckElement(Repository)) { IsEnabled = true; }
                        else { IsEnabled = false; }
                        break;

                    // TreeView menu
                    // -- nothing yet --

                    // if there is any other option, disable it by default
                    default:
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If any Project is not opened, disable all Menu options
                IsEnabled = false;
            }
        }

        // Method called when user clicks on one of the Menu options
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // Main menu options
                case menuShowCommentWindow:
                    Repository.SaveAllDiagrams();
                    commentBrowserController.initWindow();
                    break;
                case menuImportComments:
                    Repository.SaveAllDiagrams();
                    importService.ImportFromJSON();
                    break;
                case menuExportComments:
                    Repository.SaveAllDiagrams();
                    exportService.exportToJSON();
                    break;

                // Diagram menu options
                case menuAddCommentToElement:
                    this.addCommentWindow = new AddCommentWindow(Repository);
                    this.addCommentWindow.ShowDialog();
                    break;
                case changeStateToResolved:
                    UpdateController.updateSelectedElementState(Repository, "resolved");
                    break;
                case changeStateToUnresolved:
                    UpdateController.updateSelectedElementState(Repository, "unresolved");
                    break;

                // TreeView menu options
                // -- nothing yet --
            }
        }

        /*public bool getAndCheckConnector(Repository Repository) {
            Diagram diagram = Repository.GetCurrentDiagram();
            Connector connectorObject = diagram.SelectedObjects.GetAt(0);
            try
            {
                Connector c = Repository.GetConnectorByID(connectorObject.ConnectorID);
                bool res = isObservedStereotype(c);
                return res;
            }
            catch {
                return false;
            }
        }*/
        
        //Potrebujem metodu ktora pracuje s konektorom
        // Method verifies if selected object is Note Element
        public bool getAndCheckElement(Repository Repository)
        {
            Diagram diagram = Repository.GetCurrentDiagram();
            DiagramObject diagramObject = diagram.SelectedObjects.GetAt(0);
            //Connector con = diagram.SelectedConnector; goood vec pouzit
            try
            {
                Element e = Repository.GetElementByID(diagramObject.ElementID);
                bool res = isObservedStereotype(e);
                return res;
            }
            catch
            {
                return false;
            }
        }
        
        // Method Saves and Refreshs Diagram when changes were made
        public static void refreshDiagram(Repository Repository, Diagram d)
        {
            Repository.SaveDiagram(d.DiagramID);
            Repository.RefreshOpenDiagrams(true);
        }

        // Method verifies if any element in diagram is selected
        bool IsElementSelected(Repository Repository)
        {
            Diagram diagram = Repository.GetCurrentDiagram();
            if (diagram != null && diagram.SelectedObjects.Count == 1) { return true; }
            else { return false; }
        }

        // Method deletes all Note in Comment Browser Windows those were within specified Package
        // Method called when Package was deleted
        public bool EA_OnPreDeletePackage(Repository Repository, EventProperties Info)
        {
            try
            {
                EventProperty prop = Info.Get("PackageID");
                int packageID = int.Parse(prop.Value.ToString());
                Package p = Repository.GetPackageByID(packageID);

                commentBrowserController.deleteElementsWithinPackage(p);

            }
            catch(Exception) { }
            
            return true;
        }

        // Method deletes all Note in Comment Browser Windows those were within specified Diagram
        // Method called when Diagram was deleted
        public bool EA_OnPreDeleteDiagram(Repository Repository, EventProperties Info)
        {
            try
            {
                EventProperty prop = Info.Get("DiagramID");
                int diagramID = int.Parse(prop.Value.ToString());
                Diagram d = Repository.GetDiagramByID(diagramID);

                commentBrowserController.deleteElementsWithinDiagram(d);
            }
            catch (Exception) { }

            return true;
        }

        public bool EA_OnPreDeleteElement(Repository Repository, EventProperties Info)
        {
            try
            {
                EventProperty prop = Info.Get("ElementID");
                int elementID = int.Parse(prop.Value.ToString());
                Element e = Repository.GetElementByID(elementID);

                commentBrowserController.deleteElementsWithinElement(e);

                foreach (Diagram d in e.Diagrams)
                {
                    MessageBox.Show("element " + e.Name + " ma diagram " + d.Name);
                }
            }
            catch (Exception) { }

            return true;
        }

        // Method deletes all Note in Comment Browser Windows
        // Method called when Element was deleted in Diagram
        public bool EA_OnPreDeleteDiagramObject(Repository Repository, EventProperties Info)
        {
            try
            {
                EventProperty prop = Info.Get("ID");
                int elementID = int.Parse(prop.Value.ToString());
                Element e = Repository.GetElementByID(elementID);

                if (isObservedStereotype(e))
                {
                    MessageBox.Show("Zmazal som elemenet! " + e.Notes);
                    commentBrowserController.deleteElement(e.ElementGUID);
                }
            }
            catch(Exception) { }
            
            return true;
        }

        // Method notifies Add-in when new DiagramObject is created in Diagram
        public bool EA_OnPostNewDiagramObject(Repository Repository, EventProperties Info)
        {
            try
            {
                EventProperty prop = Info.Get("ID");
                int elementID = int.Parse(prop.Value.ToString());
                Element e = Repository.GetElementByID(elementID);

                if (isObservedStereotype(e))
                {
                    // when new Note is created with Drag & Drop ToolboxPanel, update State and GUID
                    UpdateController.assignTaggedValue(Repository, e.ElementGUID, "state", "unresolved");
                    UpdateController.assignTaggedValue(Repository, e.ElementGUID, "origin", e.ElementGUID);
                    Note n = new Note(e, Repository);

                    commentBrowserController.addNewElement(n);
                }
            }
            catch(Exception e) {}

            return true;
        }

        // Method notifies Add-in when any Item in Model is under focus (was clicked)
        public virtual void EA_OnContextItemChanged(Repository Repository, string GUID, ObjectType ot)
        {
            if(commentBrowserController != null)
            {
                commentBrowserController.updateElementContent(GUID);
            }
            
            if (ObjectType.otDiagramObject == ot || ObjectType.otElement == ot)
            {
                Element e = Repository.GetElementByGuid(GUID);
            }
        }

        // Method check if specified Element has observer Stereotype
        public static bool isObservedStereotype(Element e)
        {
            if (e.Stereotype.Equals("question") || e.Stereotype.Equals("warning") || e.Stereotype.Equals("error") || e.Stereotype.Equals("suggestion"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method disconnects from EA Repository and cleans mess
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
