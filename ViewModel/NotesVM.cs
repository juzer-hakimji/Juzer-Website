using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class NotesVM : BaseViewModel
    {
        public int NoteId { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter Date")]
        public string CreatedDate { get; set; }

        [Required(ErrorMessage = "Please enter Note")]
        public string NoteText { get; set; }

        public bool IsImportant { get; set; }
    }
}
