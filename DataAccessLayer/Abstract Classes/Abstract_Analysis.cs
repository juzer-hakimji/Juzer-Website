using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Analysis : BaseDAL, IDisposable
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

        public bool AddOrRemoveAdmin(string p_UserIds, bool p_IsAdmin)
        {
            try
            {
                List<string> lstUserIds = p_UserIds.Split(',').ToList();
                foreach (var UserId in lstUserIds)
                {
                    MST_UserInfo User = db.MST_UserInfo.Find(Convert.ToInt32(UserId));
                    User.IsAdmin = p_IsAdmin;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MST_UserInfo> GetAddOrRemoveAdminList(bool p_IsAdd)
        {
            return this.ExecuteDALMethod<bool, List<MST_UserInfo>>(db, (DataContext, IsAdd) =>
            {
                return IsAdd ? DataContext.MST_UserInfo.Where(x => x.IsAdmin == false).ToList() : DataContext.MST_UserInfo.Where(x => x.IsAdmin == true).ToList();
            }, p_IsAdd);
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
