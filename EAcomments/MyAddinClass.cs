using System;
using System.Windows.Forms;
using System.Collections.Generic;
using EA;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml;

namespace EAcomments
{
    public class MyAddinClass
    {
        // define menu constants
        const string menuHeader = "-&Comment Plugin";

        // define submenu for diagram location
        const string menuAddCommentToElement = "&Add comment to Element";
        const string menuRemoveCommentToElement = "&Remove comment";

        const string menuShowCommentWindow = "&Show Comment Browser";
        const string menuDebugComments = "&DEBUG";
        const string menuExportComments = "&Export";

        // define submenu for treeview location
        const string menuAddCommentToDiagram = "&Add comment to Diagram";

        // controls
        public static CommentBrowserControl uc_commentBrowser;

        // connects to EA repository each time EA opens
        public String EA_Connect(EA.Repository Repository)
        {
            return string.Empty;
        }

        // define default menu settings
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            if (MenuName == "")
                return menuHeader;

            // generate submenus according to location where user clicks
            switch (Location)
            {
                case "Diagram":
                    if (MenuName == menuHeader)
                        return new string[] { menuAddCommentToElement, menuRemoveCommentToElement };
                    break;
                case "MainMenu":
                    if (MenuName == menuHeader)
                        return new string[] { menuShowCommentWindow, menuDebugComments, menuExportComments };
                    break;
                case "TreeView":
                    if (MenuName == menuHeader)
                        return new string[] { menuAddCommentToDiagram };
                    break;
            }
            return string.Empty;
        }
        
        // verify if EA model is opened
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

        ///
        /// Called once Menu has been opened to see what menu items should active.
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the menu item
        /// <param name="IsEnabled" />boolean indicating whether the menu item is enabled
        /// <param name="IsChecked" />boolean indicating whether the menu is checked
        /// 
        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    // Diagram menu
                    case menuAddCommentToElement:
                        if (IsElementSelected(Repository)) { IsEnabled = true; }
                        else { IsEnabled = false; }
                        break;
                    case menuRemoveCommentToElement:
                        if (IsElementSelected(Repository)) { IsEnabled = true; }
                        else { IsEnabled = false; }
                        break;

                    // TreeView menu
                    case menuAddCommentToDiagram:
                        IsEnabled = true;
                        break;

                    // Main menu
                    case menuShowCommentWindow:
                        IsEnabled = true;
                        break;
                    case menuDebugComments:
                        IsEnabled = true;
                        break;
                    case menuExportComments:
                        IsEnabled = true;
                        break;

                    // if there is any other option, just disable it by default
                    default:
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }

        // This method is called when user clicks on one of the menu options
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // Diagram menu options
                case menuAddCommentToElement:
                    AddCommentWindow addCommentWindow = new AddCommentWindow(Repository);
                    addCommentWindow.Show();
                    break;

                case menuRemoveCommentToElement:
                    break;

                // Main menu options
                case menuShowCommentWindow:
                    this.showCommentWindow(Repository);
                    break;
                case menuDebugComments:
                    this.exportComments(Repository);
                    break;
                case menuExportComments:
                    ExportWindow exportWindow = new ExportWindow();
                    exportWindow.Show();
                    break;
            }
        }

        public static void refreshDiagram(Repository Repository)
        {
            // store and refresh diagram
            Diagram d = Repository.GetCurrentDiagram();
            Repository.SaveDiagram(d.DiagramID);
            Repository.RefreshOpenDiagrams(true);
        }

        private void exportComments(Repository Repository)
        {
            // SQL query gets Collection of Elements with specified stereotype from EA.Model
            List<Note> notes = new List<Note>();

            Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error'", 2);

            // loop through each element and get all required information about it
            foreach (Element e in collection)
            {
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
                if(parentID != 0) {
                    parentElement = Repository.GetElementByID(parentID);
                    parentElementName = parentElement.Name;
                    parentElementGUID = parentElement.ElementGUID;
                }

                // get package Info
                int packageID = parentDiagram.PackageID;
                Package package = Repository.GetPackageByID(packageID);
                string packageName = package.Name;
                string packageGUID = package.PackageGUID;

                Note note = new Note(e.ElementID, e.ElementGUID, e.Notes, e.Stereotype, diagramGUID, diagramName, parentElementGUID, parentElementName, packageGUID, packageName);
                notes.Add(note);
            }
            string json = JsonConvert.SerializeObject(notes, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(@"C:\Users\patom\Desktop\path.txt", json);
        }

        // shows Comment Window
        private void showCommentWindow(Repository Repository)
        {
            if(uc_commentBrowser == null)
            {
                try {
                    uc_commentBrowser = (CommentBrowserControl)Repository.AddTab("Comments Browser", "EAcomments.CommentBrowserControl");
                }
                catch {
                    MessageBox.Show("An error has occured when creating the \"Comments Browser\" Window.");
                }
            }
            else
            {
                MessageBox.Show("Addin windows is already created!");
            }
        }

        // verify if any element in diagram is selected
        bool IsElementSelected(Repository Repository)
        {
            var diagram = Repository.GetCurrentDiagram();

            if (diagram != null && diagram.SelectedObjects.Count == 1) { return true; }
            else { return false; }
        }

        // notifies user when any item in model was clicked
        //public void EA_OnNotifyContextItemModified(Repository Repository, string GUID, ObjectType ot)
        //{
        //    MessageBox.Show("you have updated" + GUID);
        //}

        // notifies user when any item in model was changed
        public void EA_OnNotifyContextItemModified(Repository Repository, string GUID, ObjectType ot){}

        // disconnects from EA repository and cleans mess
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
