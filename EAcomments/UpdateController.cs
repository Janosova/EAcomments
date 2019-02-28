using EA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAcomments
{
    public static class UpdateController
    {

        // Method changes TaggedValue of specified Element selected in Comment Browser Window
        public static void assignTaggedValue(Repository Repository, string elementGUID, string taggedValueName, string taggedValueValue)
        {
            Element e = Repository.GetElementByGuid(elementGUID);
            TaggedValue taggedValue = e.TaggedValues.GetByName(taggedValueName);
            taggedValue.Value = taggedValueValue;
            taggedValue.Update();
            e.Update();
        }

        // Method changed TaggedValue of clicked Element in Diagram
        public static void updateSelectedElementState(Repository Repository, string stateValue)
        {
            Diagram d = Repository.GetCurrentDiagram();
            DiagramObject diagramObject = d.SelectedObjects.GetAt(0);
            Element e = Repository.GetElementByID(diagramObject.ElementID);

            assignTaggedValue(Repository, e.ElementGUID, "state", stateValue);
            MyAddinClass.commentBrowserController.updateElementState(e.ElementGUID, stateValue);
        }

        // Method removes all un-connected Notes in Model 
        public static void sync(Repository Repository)
        {
            Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error' OR Stereotype='suggestion'", 2);
            
            for(short i = 0; i < collection.Count; i++)
            {
                Element e = collection.GetAt(i);
                if (e.Connectors.Count == 0)
                {
                    MyAddinClass.commentBrowserController.deleteElement(e.ElementGUID);
                    collection.DeleteAt(i, false);
                }                
            }
            collection.Refresh();
        }
    }
}
