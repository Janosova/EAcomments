using System.Collections.Generic;
using System.Windows.Forms;
using EA;

namespace EAcomments
{
    static class CommentBrowserController
    {
        public static CommentBrowserWindow uc_commentBrowser;
        private static Repository repository;
        private static Element lastClicked = null;

        public static void initWindow(Repository Repository)
        {
            repository = Repository;
            if (uc_commentBrowser == null)
            {
                try
                {
                    uc_commentBrowser = (CommentBrowserWindow)repository.AddTab("Comments Browser", "EAcomments.CommentBrowserWindow");
                    List<Note> notes = getNotes();
                    uc_commentBrowser.initExistingNotes(notes);
                }
                catch
                {
                    MessageBox.Show("An error has occured when creating Comment Browser Window.");
                }
            }
            else
            {
                MessageBox.Show("Windows is already displayed");
            }
        }

        public static void openDiagramWithGUID(string diagramGUID)
        {
            Diagram d = repository.GetDiagramByGuid(diagramGUID);
            int diagramID = d.DiagramID;
            repository.OpenDiagram(diagramID);
        }

        public static void updateElement(string GUID)
        {
            if(uc_commentBrowser != null)
            {
                if (lastClicked != null)
                {
                    Element currentlyClicked = repository.GetElementByGuid(GUID);
                    if(lastClicked.Stereotype == "question" || lastClicked.Stereotype == "warning" || lastClicked.Stereotype == "error")
                    {
                        MessageBox.Show("LastClicked has text: " + lastClicked.Notes);
                    }

                    lastClicked = currentlyClicked;
                }
                else
                {
                    lastClicked = repository.GetElementByGuid(GUID);
                }
            }
            //if (uc_commentbrowser != null)
            //{
            //    if (lastclicked == null)
            //    {
            //        lastclicked = repository.getelementbyguid(guid);
            //    }
            //    else
            //    {
            //        tu je chyba!!!!!!
            //       element currentlyclicked = repository.getelementbyguid(guid);
            //        if (lastclicked.stereotype == "question" || lastclicked.stereotype == "warning" || lastclicked.stereotype == "error")
            //        {
            //            messagebox.show("last clicked: " + lastclicked.notes);
            //            string updatedcontent = lastclicked.notes;
            //            messagebox.show("lastclicked ma text: " + updatedcontent);
            //            string lastclickedguid = lastclicked.elementguid;
            //            uc_commentbrowser.updateitem(lastclickedguid, updatedcontent);
            //        }
            //        lastclicked = currentlyclicked;
            //    }
            //}

        }

        public static List<Note> getNotes()
        {
            List<Note> notes = new List<Note>();

            Collection collection = repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error'", 2);

            foreach (Element e in collection)
            {
                // create Note
                Note note = new Note(e, repository);
                notes.Add(note);
            }

            return notes;
        }

        public static void windowClosed()
        {
            uc_commentBrowser = null;
        }
    }
}
