using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.MD5_Hash_Class;
using DataAccessLayer.Data_Access_Classes;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_User
    {
        private IBasicOperationsUser IUserObj { get; set; }
        private MST_UserInfo UserObj { get; set; }

        BL_User()
        {
            IUserObj = new DAL_User();
        }

        public bool BL_SaveUser(UserVM p_UserVM)
        {
            UserObj = new MST_UserInfo();
            UserObj.FirstName = p_UserVM.FirstName;
            UserObj.LastName = p_UserVM.LastName;
            UserObj.CountryId = p_UserVM.CountryId;
            UserObj.Email = p_UserVM.Email;
            UserObj.UserName = p_UserVM.UserName;
            UserObj.Password = new MD5Hashing().GetMd5Hash(p_UserVM.Password);
            UserObj.IsActive = true;
            UserObj.IsAdmin = false;
            UserObj.CreatedDate = DateTime.Now;
            return IUserObj.Insert(UserObj);
        }

        public bool BL_UpdateUser(UserVM p_UserVM)
        {
            UserObj = new MST_UserInfo();
            UserObj.FirstName = p_UserVM.FirstName;
            UserObj.LastName = p_UserVM.LastName;
            UserObj.CountryId = p_UserVM.CountryId;
            UserObj.Email = p_UserVM.Email;
            UserObj.UserName = p_UserVM.UserName;
            return IUserObj.Update(UserObj);
        }

        public bool BL_DeleteUser(int p_UserId)
        {
            return IUserObj.Delete(p_UserId);
        }

    }
}
