﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserDetailsVM
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}