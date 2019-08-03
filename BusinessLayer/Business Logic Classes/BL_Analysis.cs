using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Analysis
    {
        private DAL_Analysis DALObj { get; set; }

        public BL_Analysis()
        {
            DALObj = new DAL_Analysis();
        }

        public void BL_GetUserData()
        {

        }

        public bool BL_AddOrRemoveAdmin(string p_UserIds, bool p_IsAdmin)
        {
            return DALObj.AddOrRemoveAdmin(p_UserIds, p_IsAdmin);
        }

        public List<UserDetailsVM> BL_GetAddOrRemoveAdminList(bool IsAdd)
        {
            List<UserDetailsVM> UserList = new List<UserDetailsVM>();
            foreach (var item in DALObj.DAL_GetAddOrRemoveAdminList(IsAdd))
            {
                UserList.Add(new UserDetailsVM { UserId = item.UserId,Email = item.Email });
            }
            return UserList;
        }
        
    }
}
