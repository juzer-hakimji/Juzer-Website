﻿using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using DataAccessLayer.Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_User : Abstract_User
    {
        public DBContextResult<MST_UserInfo> DAL_GetUserValidity(string p_Email)
        {
            return GetUserValidity(p_Email);
        }

        public DBContextResult<bool> DAL_CheckForEmailAvailability(string p_Email)
        {
            return CheckForEmailAvailability(p_Email);
        }

        public bool DAL_SaveNewPassword(int p_UserId,string NewHashedPassword)
        {
            return SaveNewPassword(p_UserId, NewHashedPassword);
        }

        public DBContextResult<List<DEV_Country>> DAL_GetCountryList()
        {
            return GetCountryList();
        }
    }
}
