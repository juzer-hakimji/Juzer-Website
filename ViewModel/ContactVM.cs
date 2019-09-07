using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(30, ErrorMessage = "Do not enter more than 50 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Subject")]
        [StringLength(30, ErrorMessage = "Do not enter more than 100 characters")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter Message")]
        public string Message { get; set; }
    }
}
