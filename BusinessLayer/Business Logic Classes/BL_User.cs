﻿using BusinessEntities.Entities.Entity_Model;
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
using System.Web.Security;

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
            if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.Password).Equals(UserObj.Password) && UserObj.IsAdmin == true)
            {
                UserObj.UserStatus = MST_UserInfo.EnumUserStatus.AuthenticatedAdmin;
            }
            else if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.Password).Equals(UserObj.Password) && UserObj.IsAdmin == false)
            {
                UserObj.UserStatus = MST_UserInfo.EnumUserStatus.AuthenticatedUser;
            }
            else
            {
                UserObj.UserStatus = MST_UserInfo.EnumUserStatus.NonAuthenticatedUser;
            }
            return UserObj;
        }

        public bool BL_ValidatePassword(UserLoginVM p_UserLoginVM)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_UserLoginVM.Email);
            if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.Password).Equals(UserObj.Password))
            {
                return this.BL_DeleteUser(UserObj.UserId) ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool BL_CheckForEmailAvailability(string p_Email)
        {
            return new DAL_User().DAL_CheckForEmailAvailability(p_Email);
        }

        public string BL_GenerateNewPassword(int p_UserId)
        {
            string NewPassword = Membership.GeneratePassword(6, 1);
            bool result = new DAL_User().DAL_SaveNewPassword(p_UserId, new MD5Hashing().GetMd5Hash(NewPassword));
            return NewPassword;
        }
    }
}
