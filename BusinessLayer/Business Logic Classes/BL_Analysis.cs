using DataAccessLayer.Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Analysis
    {
        private DAL_Analysis DALObj { get; set; }

        BL_Analysis()
        {
            DALObj = new DAL_Analysis();
        }

        public void BL_GetUserData()
        {

        }

        public bool BL_AddOrRemoveAdmin(int p_UserId, bool p_IsAdmin)
        {
            return DALObj.AddOrRemoveAdmin(p_UserId, p_IsAdmin);
        }
    }
}
