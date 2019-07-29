using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserDetailsVM
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter FirstName")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string LastName { get; set; }

        public int CountryId { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }

    public class UserLoginVM
    {
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }

    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string ResetEmail { get; set; }
    }
}
