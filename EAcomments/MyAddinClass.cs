﻿using System;
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
        const string changeStateToResolved = "&Resolve Comment";
        const string changeStateToUnresolved = "&Unresolve Comment";

        const string menuShowCommentWindow = "&Show Comment Browser";
        const string menuImportComments = "&Import Comments";
        const string menuExportComments = "&Export Comments";

        // define submenu for treeview location
        const string menuAddCommentToDiagram = "&Add comment to Diagram";

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
                        return new string[] { menuAddCommentToElement, changeStateToResolved, changeStateToUnresolved };
                    break;
                case "MainMenu":
                    if (MenuName == menuHeader)
                        return new string[] { menuShowCommentWindow, menuImportComments, menuExportComments };
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

        // called when menu is being initialized
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
                    case changeStateToResolved:
                        if (getAndCheckElement(Repository)) { IsEnabled = true; }
                        else { IsEnabled = false; }
                        break;
                    case changeStateToUnresolved:
                        if (getAndCheckElement(Repository)) { IsEnabled = true; }
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
                    case menuImportComments:
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
                case changeStateToResolved:
                    UpdateController.updateSelectedElementState(Repository, "resolved");
                    break;
                case changeStateToUnresolved:
                    UpdateController.updateSelectedElementState(Repository, "unresolved");
                    break;

                // Main menu options
                case menuShowCommentWindow:
                    CommentBrowserController.initWindow(Repository);
                    break;
                case menuImportComments:
                    ImportService.ImportFromJSON(Repository);
                    break;
                case menuExportComments:
                    ExportService.exportToJson(Repository);
                    break;
            }
        }
        
        // verify if selected object is Note
        public bool getAndCheckElement(Repository Repository)
        {
            Diagram diagram = Repository.GetCurrentDiagram();
            DiagramObject diagramObject = diagram.SelectedObjects.GetAt(0);
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

        public static void refreshDiagram(Repository Repository, Diagram d)
        {
            // store and refresh diagram
            Repository.SaveDiagram(d.DiagramID);
            Repository.RefreshOpenDiagrams(true);
        }

        // verify if any element in diagram is selected
        bool IsElementSelected(Repository Repository)
        {
            Diagram diagram = Repository.GetCurrentDiagram();
            if (diagram != null && diagram.SelectedObjects.Count == 1) { return true; }
            else { return false; }
        }

        // notifies Add-in when new DiagramObject is created in Diagram
        public bool EA_OnPostNewDiagramObject(Repository Repository, EventProperties Info)
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
                CommentBrowserController.addNewElement(n);
            }

            return true;
        }

        // notifies Add-in when any Item in Model is under focus (was clicked)
        public virtual void EA_OnContextItemChanged(Repository Repository, string GUID, ObjectType ot)
        {
            CommentBrowserController.updateElementContent(GUID);
            if(ObjectType.otDiagramObject == ot || ObjectType.otElement == ot)
            {
                Element e = Repository.GetElementByGuid(GUID);
            }
        }

        public bool EA_OnPreDeleteDiagramObject(Repository Repository, EventProperties Info)
        {
            EventProperty prop = Info.Get("ID");
            int elementID = int.Parse(prop.Value.ToString());
            Element e = Repository.GetElementByID(elementID);

            if(isObservedStereotype(e))
            {
                CommentBrowserController.deleteElement(e.ElementGUID);
            }
            return true;
        }

        /*
                public bool EA_OnPreDeleteDiagramObject(Repository Repository, EventProperties Info)
                {}
                    EventProperty prop = Info.Get("ID");
                    int elementID = int.Parse(prop.Value.ToString());
                    Element e = Repository.GetElementByID(elementID);
                    Diagram d = Repository.GetCurrentDiagram();

                    foreach (Connector c in e.Connectors)
                    {
                        int connectedToID = 0;
                        if (c.SupplierID.Equals(elementID)) connectedToID = c.ClientID;
                        else if (c.ClientID.Equals(elementID)) connectedToID = c.SupplierID;

                        Element relatedElement = Repository.GetElementByID(connectedToID);
                        MessageBox.Show("related element stereotype" + relatedElement.Stereotype);
                        if(relatedElement.Stereotype == "question")
                        {   
                            MessageBox.Show("pripojeny element je question");
                            if (relatedElement.Connectors.Count < 2)
                            {
                                for (short i = 0; i < d.DiagramObjects.Count; i++)
                                {
                                    DiagramObject diagramObject = d.DiagramObjects.GetAt(i);
                                    MessageBox.Show("Porovnavam " + diagramObject.ElementID + " s " + connectedToID);
                                    if (diagramObject.ElementID == connectedToID)
                                    {
                                        Element el = Repository.GetElementByID(connectedToID);
                                        MessageBox.Show("Idem mazat aj " + el.Notes);
                                        d.DiagramObjects.DeleteAt(i, false);
                                    }
                                }
                                refreshDiagram(Repository, d);
                                return false;
                            }
                        }
                    }

                    return true;
                }
        */

        public static bool isObservedStereotype(Element e)
        {
            if (e.Stereotype.Equals("question") || e.Stereotype.Equals("warning") || e.Stereotype.Equals("error"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // disconnects from EA repository and cleans mess
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
