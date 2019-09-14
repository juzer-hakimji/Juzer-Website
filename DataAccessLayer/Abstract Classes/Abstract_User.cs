using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using DataAccessLayer.Data_Model;
using DataAccessLayer.Database_Utilities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public class Abstract_User : BaseDAL, IBasicOperationsUser, IDisposable
    {
        private JuzerWebsiteEntities db;

        public Abstract_User()
        {
            db = new JuzerWebsiteEntities();
        }

        public DBContextResult<MST_UserInfo> Insert(MST_UserInfo p_MST_UserInfo)
        {
            return ExecuteDALMethod(db, (DataContext, P_MST_UserInfo) =>
            {
                DataContext.MST_UserInfo.Add(P_MST_UserInfo);
                DataContext.SaveChanges();
                return DataContext.MST_UserInfo.OrderByDescending(x => x.UserId).FirstOrDefault();
            }, p_MST_UserInfo);
        }

        public DBContextResult<object> Update(MST_UserInfo p_MST_UserInfo)
        {
            return ExecuteDALMethod<MST_UserInfo, object>(db, (DataContext, P_MST_UserInfo) =>
            {
                MST_UserInfo UserObj = DataContext.MST_UserInfo.Find(P_MST_UserInfo.UserId);
                UserObj.FirstName = P_MST_UserInfo.FirstName;
                UserObj.LastName = P_MST_UserInfo.LastName;
                UserObj.CountryId = P_MST_UserInfo.CountryId;
                UserObj.Email = P_MST_UserInfo.Email;
                UserObj.Password = P_MST_UserInfo.Password;
                DataContext.SaveChanges();
                return null;
            }, p_MST_UserInfo);
        }

        public DBContextResult<object> Delete(int p_UserId)
        {
            return ExecuteDALMethod<int, object>(db, (DataContext, P_UserId) =>
             {
                 MST_UserInfo Obj = DataContext.MST_UserInfo.Find(P_UserId);
                 Obj.IsActive = false;
                 DataContext.SaveChanges();
                 return null;
             }, p_UserId);
        }

        public DBContextResult<MST_UserInfo> GetUserValidity(string p_Email)
        {
            return ExecuteDALMethod(db, (DataContext, P_Email) =>
             {
                 MST_UserInfo UserObj = DataContext.MST_UserInfo.SingleOrDefault(x => x.Email.Equals(P_Email) && x.IsActive == true);
                 return UserObj;
             }, p_Email);
        }

        public DBContextResult<bool> CheckForEmailAvailability(string p_Email)
        {
            return ExecuteDALMethod(db, (DataContext, P_Email) =>
            {
                return DataContext.MST_UserInfo.Any(x => x.Email.Equals(P_Email));
            }, p_Email);
        }

        public bool SaveNewPassword(string p_Email, string NewHashedPassword)
        {
            try
            {
                MST_UserInfo MST_UserInfo = db.MST_UserInfo.FirstOrDefault(x => x.Email.Equals(p_Email));
                MST_UserInfo.Password = NewHashedPassword;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new LogError(ex);
                return false;
            }
        }

        public DBContextResult<List<DEV_Country>> GetCountryList()
        {
            return ExecuteDALMethod(db, (DataContext, p_obj) =>
            {
                return DataContext.DEV_Country.ToList();
            }, new object());
        }

        public DBContextResult<bool> SaveContactInfo(CustomerContactInfo InfoObj)
        {
            return ExecuteDALMethod(db, (DataContext, p_InfoObj) =>
            {
                DataContext.CustomerContactInfo.Add(p_InfoObj);
                DataContext.SaveChanges();
                return true;
            }, InfoObj);
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
