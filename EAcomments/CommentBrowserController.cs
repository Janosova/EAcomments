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

        // Method called on double click event on single row
        public static void openDiagramWithGUID(string diagramGUID)
        {
            try
            {
                Diagram d = repository.GetDiagramByGuid(diagramGUID);
                repository.OpenDiagram(d.DiagramID);
            }
            catch
            {
                MessageBox.Show("Could not open Note location, check if location exists");
            }
            
        }

        // Method called when new Note is posted into Model
        public static void addNewElement(Note note)
        {
            if (uc_commentBrowser != null)
            {
                uc_commentBrowser.addItem(note);
            }
        }

        // Method called on sync event
        public static void deleteElement(string elementGUID)
        {
            if(uc_commentBrowser != null)
            {
                uc_commentBrowser.deleteElement(elementGUID);
            }
            
        }

        public static void deleteElementsWithinPackage(Package p)
        {
            if (uc_commentBrowser != null)
            {
                DataGridView dgw = uc_commentBrowser.dataGridView;

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    Note n = (Note)row.DataBoundItem;
                    if (n.packageGUID.Equals(p.PackageGUID))
                    {
                        dgw.Rows.Remove(row);
                    }
                }
                dgw.Refresh();
                dgw.Update();
            }
        }

        public static void deleteElementsWithinDiagram(Diagram d)
        {
            if (uc_commentBrowser != null)
            {
                DataGridView dgw = uc_commentBrowser.dataGridView;

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    Note n = (Note)row.DataBoundItem;
                    // update Row in Comment Browser Window
                    if (n.diagramGUID.Equals(d.DiagramGUID))
                    {
                        dgw.Rows.Remove(row);
                    }
                }
                dgw.Refresh();
                dgw.Update();
            }
        }

        public static void deleteElementsWithinElement(Element e)
        {
            if (uc_commentBrowser != null)
            {
                DataGridView dgw = uc_commentBrowser.dataGridView;
                short i = 0;
                foreach(Diagram d in e.Diagrams)
                {
                    foreach (DataGridViewRow row in dgw.Rows)
                    {
                        Note n = (Note)row.DataBoundItem;
                        // update Row in Comment Browser Window
                        if (n.diagramGUID.Equals(d.DiagramGUID))
                        {
                            dgw.Rows.Remove(row);
                        }
                    }
                    e.Diagrams.DeleteAt(i++, true);
                }
                                
                dgw.Refresh();
                dgw.Update();
            }
        }

        // Method called when TaggedValue of single Note has been changed
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

        // Method changes Checkbox Value andcalled when TaggedValue of single Note has been changed
        public static void updateElementState(DataGridViewCellEventArgs e, DataGridView dataGridView1)
        {
            if(uc_commentBrowser != null)
            {
                // Get clicked DataGridViewCell and verify if it's CheckBoxCell
                DataGridViewCell dataCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Type cellType = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType();
                if (cellType.Name.Equals("DataGridViewCheckBoxCell"))
                {
                    // Get clicked Checkbox
                    DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    checkbox.TrueValue = true;
                    checkbox.FalseValue = false;

                    // Get selected Row and get selected Note
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    Note n = (Note)row.DataBoundItem;

                    // Change state of single Note in Window according its actual Value
                    if (checkbox.Value == checkbox.FalseValue || checkbox.Value == null)
                    {
                        checkbox.Value = checkbox.TrueValue;
                        UpdateController.assignTaggedValue(repository, n.GUID, "state", "resolved");
                    }
                    else
                    {
                        checkbox.Value = null;
                        UpdateController.assignTaggedValue(repository, n.GUID, "state", "unresolved");
                    }
                }
            }
        }

        // Method called when Note content has been changed in Model
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

        // Method called when window is being initialized
        public static List<Note> getNotes()
        {
            List<Note> notes = new List<Note>();

            // get Collection of Notes and loop through it and add every Note DataGridView's DataSource
            Collection collection = repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error'", 2);

            foreach (Element e in collection)
            {
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
