﻿using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_User : Abstract_User
    {
        public MST_UserInfo DAL_GetUserValidity(string p_Email)
        {
            return this.GetUserValidity(p_Email);
        }
    }
}