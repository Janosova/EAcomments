using System;
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
                    uc_commentBrowser.initExistingNotes(notes, Repository);
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

        public static void refreshWindow()
        {
            if(uc_commentBrowser != null)
            {
                List<Note> notes = getNotes();
                uc_commentBrowser.clearWindow();
                uc_commentBrowser.initExistingNotes(notes, repository);
            }
        }

        public static void openDiagramWithGUID(string diagramGUID)
        {
            Diagram d = repository.GetDiagramByGuid(diagramGUID);
            int diagramID = d.DiagramID;
            repository.OpenDiagram(diagramID);
        }

        public static void addNewElement(Note note)
        {
            if (uc_commentBrowser != null)
            {
                uc_commentBrowser.addItem(note);
            }
        }

        public static void deleteElement(string elementGUID)
        {
            if(uc_commentBrowser != null)
            {
                uc_commentBrowser.deleteElement(elementGUID);
            }
            
        }

        public static void updateElementState(string elementGUID, string stateValue)
        {
            if(uc_commentBrowser != null)
            {
                DataGridView dgw = uc_commentBrowser.dataGridView;

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    Note n = (Note)row.DataBoundItem;
                    if (n.GUID.Equals(elementGUID))
                    {
                        DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)row.Cells[9];
                        checkbox.TrueValue = true;
                        checkbox.FalseValue = false;
                        if (stateValue.Equals("resolved"))
                        {
                            checkbox.Value = checkbox.TrueValue;
                        }
                        else if (stateValue.Equals("unresolved"))
                        {
                            checkbox.Value = checkbox.FalseValue;
                        }
                        break;
                    }
                }
                dgw.Refresh();
                dgw.Update();
            }
            
        }

        public static void updateElementState(DataGridViewCellEventArgs e, DataGridView dataGridView1)
        {
            if(uc_commentBrowser != null)
            {
                // get clicked DataGridViewCell and verify if it's CheckBoxCell
                DataGridViewCell dataCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Type cellType = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType();
                if (cellType.Name.Equals("DataGridViewCheckBoxCell"))
                {
                    // get clicked Checkbox
                    DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    checkbox.TrueValue = true;
                    checkbox.FalseValue = false;

                    // get selected Row and get selected Note
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    Note n = (Note)row.DataBoundItem;

                    if (checkbox.Value == checkbox.FalseValue || checkbox.Value == null)
                    {
                        // update Note state
                        checkbox.Value = checkbox.TrueValue;
                        UpdateController.assignTaggedValue(repository, n.GUID, "state", "resolved");
                    }
                    else
                    {
                        // update Note state
                        checkbox.Value = null;
                        UpdateController.assignTaggedValue(repository, n.GUID, "state", "unresolved");
                    }
                }
            }
        }

        public static void updateElementContent(string currentElementGUID)
        {
            if(uc_commentBrowser != null)
            {
                if (lastClicked != null)
                {
                    if(MyAddinClass.isObservedStereotype(lastClicked))
                    {
                        Element e = repository.GetElementByGuid(lastClicked.ElementGUID);
                        uc_commentBrowser.updateContent(e.ElementGUID, currentElementGUID, e.Notes);
                    }
                    lastClicked = repository.GetElementByGuid(currentElementGUID);
                }
                else
                {
                    lastClicked = repository.GetElementByGuid(currentElementGUID);
                }
            }
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
