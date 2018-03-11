using System;
using System.Windows.Forms;
using EA;

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
        const string test2 = "&Text2";

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
                        return new string[] { menuShowCommentWindow };
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
                EA.Collection c = Repository.Models;
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
            }
        }

/* add comment old method
        public static void addComment(Repository Repository, string commentText)
        {
            // get current Diagram, Package and selected Item
            Diagram diagram = Repository.GetCurrentDiagram();
            Package pack = Repository.GetTreeSelectedPackage();
            DiagramObject selectedItem = diagram.SelectedObjects.GetAt(0);

            // get selected item coordinates
            int l = selectedItem.left;
            int b = selectedItem.bottom;

            // create new node
            Element newNote;
            newNote = pack.Elements.AddNew("Note type", "Note");
            newNote.Notes = commentText;
            newNote.SetAppearance(1, 0, 15960070);
            newNote.Update();

            // connect node from TreeView with Diagram Element
            DiagramObject obj = diagram.DiagramObjects.AddNew("Neznamy nazov", "Note");
            obj.ElementID = newNote.ElementID;
            obj.Update();

            // add connector between two Diagram Elements
            Connector connector = newNote.Connectors.AddNew("", "Association");
            connector.SupplierID = selectedItem.ElementID;
            connector.Update();

            // add new Comment to commentWindow
            //uc_commentWindow.ListBox2.Items.Add(new Note(1, commentText));

            // update repository to see changes
            Repository.RefreshOpenDiagrams(true);
        }
*/

        public static void refreshDiagram(Repository Repository)
        {
            Repository.RefreshOpenDiagrams(true);
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
        bool IsElementSelected(EA.Repository Repository)
        {
            var diagram = Repository.GetCurrentDiagram();

            if (diagram != null && diagram.SelectedObjects.Count == 1) { return true; }
            else { return false; }
        }

        // disconnects from EA repository and cleans mess
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
