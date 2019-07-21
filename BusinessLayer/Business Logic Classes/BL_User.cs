using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.MD5_Hash_Class;
using BusinessLayer.User_Status;
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

        public BL_User()
        {
            IUserObj = new DAL_User();
        }

        public MST_UserInfo BL_SaveUser(UserDetailsVM p_UserVM)
        {
            UserObj = new MST_UserInfo
            {
                FirstName = p_UserVM.FirstName,
                LastName = p_UserVM.LastName,
                CountryId = p_UserVM.CountryId,
                Email = p_UserVM.Email,
                Password = new MD5Hashing().GetMd5Hash(p_UserVM.Password),
                IsActive = true,
                IsAdmin = false,
                CreatedDate = DateTime.UtcNow
            };
            return IUserObj.Insert(UserObj);
        }

        public bool BL_UpdateUser(UserDetailsVM p_UserVM)
        {
            UserObj = new MST_UserInfo
            {
                FirstName = p_UserVM.FirstName,
                LastName = p_UserVM.LastName,
                CountryId = p_UserVM.CountryId,
                Email = p_UserVM.Email
            };
            return IUserObj.Update(UserObj);
        }

        public bool BL_DeleteUser(int p_UserId)
        {
            return IUserObj.Delete(p_UserId);
        }

        public MST_UserInfo BL_GetUserValidity(UserLoginVM p_UserLoginVM)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_UserLoginVM.Email);
            if (p_UserLoginVM.Password.Equals(UserObj.Password) && UserObj.IsAdmin == true)
            {
                UserObj.UserStatus = MST_UserInfo.EnumUserStatus.AuthenticatedAdmin;
            }
            else if (p_UserLoginVM.Password.Equals(UserObj.Password) && UserObj.IsAdmin == false)
            {
                UserObj.UserStatus = MST_UserInfo.EnumUserStatus.AuthenticatedUser;
            }
            else
            {
                UserObj.UserStatus = MST_UserInfo.EnumUserStatus.NonAuthenticatedUser;
            }
            return UserObj;
        }
    }
}
