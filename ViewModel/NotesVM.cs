using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class NotesVM : BaseViewModel
    {
        public int NoteId { get; set; }
        public string Subject { get; set; }
        public string CreatedDate { get; set; }
        public string NoteText { get; set; }
        public bool IsImportant { get; set; }
    }
}
