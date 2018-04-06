using EA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAcomments
{
    static class UpdateController
    {
        public static void assignTaggedValue(Repository Repository, string elementGUID, string taggedValueName, string taggedValueValue)
        {
            Element el = Repository.GetElementByGuid(elementGUID);
            TaggedValue taggedValue = el.TaggedValues.GetByName(taggedValueName);
            taggedValue.Value = taggedValueValue;
            taggedValue.Update();
            el.Update();
        }

        public static void updateSelectedElementState(Repository Repository, string stateValue)
        {
            Diagram d = Repository.GetCurrentDiagram();
            DiagramObject diagramObject = d.SelectedObjects.GetAt(0);
            Element e = Repository.GetElementByID(diagramObject.ElementID);

            assignTaggedValue(Repository, e.ElementGUID, "state", stateValue);
            CommentBrowserController.updateElementState(e.ElementGUID, stateValue);
        }

        public static void sync(Repository Repository)
        {
            Collection collection = Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error'", 2);
            
            foreach(Element e in collection)
            {
                MessageBox.Show("element " + e.Notes + "ma " + e.Connectors.Count + " konektorov");
                if(e.Connectors.Count == 0)
                {
                    MessageBox.Show("element " + e.Notes + " nema konektory idem ho zmazat");
                    string diagramData = Repository.SQLQuery("SELECT t_diagram.ea_guid FROM t_diagram, t_diagramobjects WHERE t_diagramobjects.diagram_id = t_diagram.diagram_id AND t_diagramobjects.Object_ID =" + e.ElementID.ToString());
                    string diagramGUID = XMLParser.parseXML("ea_guid", diagramData);
                    Diagram d = null;
                    try
                    {
                        d = Repository.GetDiagramByGuid(diagramGUID);
                    } catch { }
                    

                    for (short i = 0; i < d.DiagramObjects.Count; i++)
                    {
                        DiagramObject diagramObject = d.DiagramObjects.GetAt(i);
                        MessageBox.Show("Porovnavam " + diagramObject.ElementID + " spolu s " + e.ElementID + " element sa vola " + e.Notes);
                        if (diagramObject.ElementID == e.ElementID)
                        {
                            Element el = Repository.GetElementByID(e.ElementID);
                            d.DiagramObjects.Delete(i);
                            d.Update();
                            Repository.RefreshOpenDiagrams(true);
                            if (MyAddinClass.isObservedStereotype(e))
                            {
                                CommentBrowserController.deleteElement(e.ElementGUID);
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
