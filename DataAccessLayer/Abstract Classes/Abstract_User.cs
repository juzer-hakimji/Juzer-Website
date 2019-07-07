using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public class Abstract_User : BaseDAL , IBasicOperationsUser , IDisposable
    {
        private JuzerWebsiteEntities db;

        public Abstract_User()
        {
            db = new JuzerWebsiteEntities();
        }

        public bool Insert(MST_UserInfo p_MST_UserInfo)
        {
            return this.ExecuteDALMethod<MST_UserInfo>(db, (DataContext, P_MST_UserInfo) =>
            {
                db.MST_UserInfo.Add(P_MST_UserInfo);
                db.SaveChanges();
                return true;
            }, p_MST_UserInfo);
        }

        public bool Update(MST_UserInfo p_MST_UserInfo)
        {
            return this.ExecuteDALMethod<MST_UserInfo>(db, (DataContext, P_MST_UserInfo) =>
            {
                MST_UserInfo UserObj = DataContext.MST_UserInfo.Find(P_MST_UserInfo.UserId);
                UserObj.FirstName = P_MST_UserInfo.FirstName;
                UserObj.LastName = P_MST_UserInfo.LastName;
                UserObj.CountryId = P_MST_UserInfo.CountryId;
                UserObj.Email = P_MST_UserInfo.Email;
                UserObj.UserName = P_MST_UserInfo.UserName;
                UserObj.Password = P_MST_UserInfo.Password;
                DataContext.SaveChanges();
                return true;
            }, p_MST_UserInfo);
        }

        public bool Delete(int p_UserId)
        {
            MST_UserInfo UserObj = new MST_UserInfo();
            UserObj.UserId = p_UserId;
            return this.ExecuteDALMethod<MST_UserInfo>(db, (DataContext, P_MST_UserInfo) =>
            {
                MST_UserInfo Obj = DataContext.MST_UserInfo.Find(P_MST_UserInfo.UserId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, UserObj);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        ~Abstract_User()
        {
            Dispose();
        }
    }
}
