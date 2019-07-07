﻿using BusinessEntities.Entities.Entity_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Analysis : IDisposable
    {
        JuzerWebsiteEntities db;

        public Abstract_Analysis()
        {
            db = new JuzerWebsiteEntities();
        }

        public void GetUserData()
        {
            //get number of users and new users monthwise and yearwise using stored procedure
        }

        public bool AddOrRemoveAdmin(int p_UserId, bool p_IsAdmin)
        {
            try
            {
                MST_UserInfo User = db.MST_UserInfo.Find(p_UserId);
                if (p_IsAdmin)
                    User.IsAdmin = true;
                else
                    User.IsAdmin = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        ~Abstract_Analysis()
        {
            Dispose();
        }
    }
}
