using DataAccessLayer.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Analysis : Abstract_Analysis
    {
        public void DAL_GetUserData()
        {

        }

        public bool DAL_AddOrRemoveAdmin(int p_UserId, bool p_IsAdmin)
        {
            return this.AddOrRemoveAdmin(p_UserId, p_IsAdmin);
        }
    }
}

