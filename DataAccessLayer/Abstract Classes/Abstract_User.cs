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
            return this.ExecuteDALMethod<MST_UserInfo, bool>(db, (DataContext, P_MST_UserInfo) =>
            {
                db.MST_UserInfo.Add(P_MST_UserInfo);
                db.SaveChanges();
                return true;
            }, p_MST_UserInfo);
        }

        public bool Update(MST_UserInfo p_MST_UserInfo)
        {
            return this.ExecuteDALMethod<MST_UserInfo, bool>(db, (DataContext, P_MST_UserInfo) =>
            {
                MST_UserInfo UserObj = DataContext.MST_UserInfo.Find(P_MST_UserInfo.UserId);
                UserObj.FirstName = P_MST_UserInfo.FirstName;
                UserObj.LastName = P_MST_UserInfo.LastName;
                UserObj.CountryId = P_MST_UserInfo.CountryId;
                UserObj.Email = P_MST_UserInfo.Email;
                UserObj.Password = P_MST_UserInfo.Password;
                DataContext.SaveChanges();
                return true;
            }, p_MST_UserInfo);
        }

        public bool Delete(int p_UserId)
        {
            MST_UserInfo UserObj = new MST_UserInfo
            {
                UserId = p_UserId
            };
            return this.ExecuteDALMethod<MST_UserInfo,bool>(db, (DataContext, P_MST_UserInfo) =>
            {
                MST_UserInfo Obj = DataContext.MST_UserInfo.Find(P_MST_UserInfo.UserId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, UserObj);
        }

        public MST_UserInfo GetUserValidity(string p_Email)
        {
            return this.ExecuteDALMethod<string,MST_UserInfo>(db,(DataContext,P_Email) => 
            {
                var UserObj = DataContext.MST_UserInfo.Where(x => x.Email == P_Email).Select(x => new { x.UserId,x.Password, x.IsAdmin }).SingleOrDefault();
                return new MST_UserInfo { Password = UserObj.Password , IsAdmin = UserObj.IsAdmin };
            },p_Email);
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
