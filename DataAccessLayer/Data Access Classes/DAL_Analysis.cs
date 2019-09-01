using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using DataAccessLayer.Data_Model;
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

        public bool DAL_AddOrRemoveAdmin(List<string> p_UserIds, bool p_IsAdmin)
        {
            return AddOrRemoveAdmin(p_UserIds, p_IsAdmin);
        }

        public DBContextResult<List<MST_UserInfo>> DAL_GetAddOrRemoveAdminList(bool IsAdd)
        {
            return GetAddOrRemoveAdminList(IsAdd);
        }
    }
}

