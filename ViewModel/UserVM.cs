﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainUserVM
    {
        public UserDetailsVM UserDetailsVM { get; set; }
        public UserLoginVM UserLoginVM { get; set; }
        public ResetPasswordVM ResetPasswordVM { get; set; }
        public ContactVM ContactVM { get; set; }
    }

    public class UserDetailsVM
    {
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Please enter FirstName")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string SignUpEmail { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [MinLength(7, ErrorMessage = "Please enter more than 7 characters")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string SignUpPassword { get; set; }

        public List<CountryVM> CountryList { get; set; }

        public UserDetailsVM()
        {
            CountryList = new List<CountryVM>();
        }
    }

    public class UserLoginVM
    {
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string LoginPassword { get; set; }
    }

    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string ResetEmail { get; set; }
    }

    public class ChangePasswordVM :BaseViewModel
    {
        [Required(ErrorMessage = "Please enter Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter New Password")]
        [MinLength(7, ErrorMessage = "Please enter more than 7 characters")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string NewPassword { get; set; }
    }
}
