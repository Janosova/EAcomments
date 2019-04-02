using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EA;

namespace EAcomments
{
    public class CommentBrowserController
    {
        CommentBrowserWindow uc_commentBrowser;
        private Repository Repository = null;
        private Element lastClicked = null;

        public CommentBrowserController(Repository Repository)
        {
            this.Repository = Repository;
        }
        
        public void initWindow()
        {
            if (uc_commentBrowser == null)
            {
                try
                {
                    uc_commentBrowser = (CommentBrowserWindow)this.Repository.AddTab("Comments Browser", "EAcomments.CommentBrowserWindow");
                    List<Note> notes = getNotes();
                    uc_commentBrowser.initExistingNotes(notes, this.Repository);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error has occured when creating Comment Browser Window.");
                }
            }
            else
            {
                MessageBox.Show("Windows is already displayed");
            }
        }

        public void refreshWindow()
        {
            if(this.uc_commentBrowser != null)
            {
                List<Note> notes = getNotes();
                this.uc_commentBrowser.clearWindow();
                this.uc_commentBrowser.initExistingNotes(notes, this.Repository);
            }
        }

        // Method called on double click event on single row
        public void openDiagramWithGUID(string diagramGUID)
        {
            try
            {
                Diagram d = this.Repository.GetDiagramByGuid(diagramGUID);
                this.Repository.OpenDiagram(d.DiagramID);
            }
            catch
            {
                MessageBox.Show("Could not open Note location, check if location exists");
            }
            
        }

        // Method called when new Note is posted into Model
        public void addNewElement(Note note)
        {
            if (this.uc_commentBrowser != null)
            {
                //this.uc_commentBrowser.addItem(note);
                this.uc_commentBrowser.bindingSourse.Add(note);
                this.uc_commentBrowser.dataGridView.DataSource = this.uc_commentBrowser.bindingSourse;

            }
        }

        // Method called on sync event
        public void deleteElement(string elementGUID)
        {
            if(uc_commentBrowser != null)
            {
                //uc_commentBrowser.deleteElement(elementGUID);
                foreach (DataGridViewRow row in this.uc_commentBrowser.dataGridView.Rows)
                {
                    Note n = (Note)row.DataBoundItem;
                    if (n.GUID.Equals(elementGUID))
                    {
                        this.uc_commentBrowser.dataGridView.Rows.Remove(row);
                    }
                }
                this.uc_commentBrowser.dataGridView.Refresh();
                this.uc_commentBrowser.dataGridView.Update();
            }
        }

        public void deleteElementsWithinPackage(Package p)
        {
            if (this.uc_commentBrowser != null)
            {
                DataGridView dgw = this.uc_commentBrowser.dataGridView;

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

        public void deleteElementsWithinDiagram(Diagram d)
        {
            if (this.uc_commentBrowser != null)
            {
                DataGridView dgw = this.uc_commentBrowser.dataGridView;

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

        public void deleteElementsWithinElement(Element e)
        {
            if (this.uc_commentBrowser != null)
            {
                DataGridView dgw = this.uc_commentBrowser.dataGridView;
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
        public void updateElementState(string elementGUID, string stateValue)
        {
            if (this.uc_commentBrowser != null)
            {
                DataGridView dgw = this.uc_commentBrowser.dataGridView;

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
        public void updateElementState(DataGridViewCellEventArgs e, DataGridView dataGridView1)
        {
            if(this.uc_commentBrowser != null)
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
                        UpdateController.assignTaggedValue(this.Repository, n.GUID, "state", "resolved");
                    }
                    else
                    {
                        checkbox.Value = null;
                        UpdateController.assignTaggedValue(this.Repository, n.GUID, "state", "unresolved");
                    }
                }
            }
        }

        // Method called when Note content has been changed in Model
        public void updateElementContent(string currentElementGUID)
        {
            if(this.uc_commentBrowser != null)
            {
                if (lastClicked != null)
                {
                    if(MyAddinClass.isObservedStereotype(lastClicked))
                    {
                        Element e = this.Repository.GetElementByGuid(lastClicked.ElementGUID);
                        this.uc_commentBrowser.updateContent(e.ElementGUID, currentElementGUID, e.Notes);
                    }
                    lastClicked = this.Repository.GetElementByGuid(currentElementGUID);
                }
                else
                {
                    lastClicked = this.Repository.GetElementByGuid(currentElementGUID);
                }
            }
        }

        // Method called when window is being initialized
        public List<Note> getNotes()
        {
            Collection collection = null;
            List<Note> notes = new List<Note>();

            // get Collection of Notes and loop through it and add every Note DataGridView's DataSource
            collection = this.Repository.GetElementSet("SELECT Object_ID FROM t_object WHERE Stereotype='question' OR Stereotype='warning' OR Stereotype='error' OR Stereotype='suggestion' OR Stereotype='question Cardinality' OR Stereotype='warning Cardinality' OR Stereotype='error Cardinality' OR Stereotype='suggestion Cardinality'", 2);

            foreach (Element e in collection)
            {
                Note note = new Note(e, this.Repository);
                notes.Add(note);
            }

            return notes;
        }

        public void windowClosed()
        {
            this.uc_commentBrowser = null;
        }
    }
}
