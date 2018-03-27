using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAcomments
{
    public class NoteRepository
    {
        public static List<Note> notes { get; set; }

        public static void show()
        {
            foreach(Note n in notes)
            {
                MessageBox.Show(n.content);
            }
        }
    }
}
