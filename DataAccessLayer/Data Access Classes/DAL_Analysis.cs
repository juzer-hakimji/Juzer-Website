using BusinessEntities.Entities.Entity_Model;
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

        public bool DAL_AddOrRemoveAdmin(string p_UserIds, bool p_IsAdmin)
        {
            return this.AddOrRemoveAdmin(p_UserIds, p_IsAdmin);
        }

        public List<MST_UserInfo> DAL_GetAddOrRemoveAdminList(bool IsAdd)
        {
            return this.GetAddOrRemoveAdminList(IsAdd);
        }
    }
}

