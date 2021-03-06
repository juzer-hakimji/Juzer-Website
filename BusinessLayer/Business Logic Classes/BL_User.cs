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
using BusinessLayer.TransactionResultModel;

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

        public TransactionResult<MST_UserInfo> BL_SaveUser(UserDetailsVM p_UserVM)
        {
            if (BL_CheckForEmailAvailability(p_UserVM.SignUpEmail))
            {
                return new TransactionResult<MST_UserInfo>
                {
                    Success = false,
                    RedirectURL = "/Home/Index",
                    Message = "Email already Exists,Please use forgot password section to generate new password."
                };
            }
            else
            {
                return new TransactionResult<MST_UserInfo>
                {
                    Success = true,
                    Data = IUserObj.Insert(new MST_UserInfo
                    {
                        FirstName = p_UserVM.FirstName,
                        LastName = p_UserVM.LastName,
                        CountryId = p_UserVM.CountryId,
                        Email = p_UserVM.SignUpEmail,
                        Password = new MD5Hashing().GetMd5Hash(p_UserVM.SignUpPassword),
                        IsActive = true,
                        IsAdmin = false,
                        CreatedDate = DateTime.UtcNow
                    }).Data
                };
            }
        }

        public bool BL_UpdateUser(UserDetailsVM p_UserVM)
        {
            return IUserObj.Update(new MST_UserInfo
            {
                FirstName = p_UserVM.FirstName,
                LastName = p_UserVM.LastName,
                CountryId = p_UserVM.CountryId,
                Email = p_UserVM.SignUpEmail
            }).TransactionResult;
        }

        public bool BL_DeleteUser(int p_UserId)
        {
            return IUserObj.Delete(p_UserId).TransactionResult;
        }

        public TransactionResult<MST_UserInfo> BL_GetUserValidity(UserLoginVM p_UserLoginVM)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_UserLoginVM.LoginEmail).Data;
            if (UserObj != null)
            {
                if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.LoginPassword).Equals(UserObj.Password) && UserObj.IsAdmin == true)
                {
                    UserObj.UserStatus = MST_UserInfo.EnumUserStatus.AuthenticatedAdmin;
                }
                else if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.LoginPassword).Equals(UserObj.Password) && UserObj.IsAdmin == false)
                {
                    UserObj.UserStatus = MST_UserInfo.EnumUserStatus.AuthenticatedUser;
                }
                else
                {
                    UserObj.UserStatus = MST_UserInfo.EnumUserStatus.NonAuthenticatedUser;
                }
                return new TransactionResult<MST_UserInfo>
                {
                    Success = true,
                    Data = UserObj
                };
            }
            else
            {
                return new TransactionResult<MST_UserInfo>
                {
                    Success = false,
                    Message = "Incorrect Email"
                };
            }
        }

        public TransactionResult<object> BL_ValidatePasswordAndDeleteUser(UserLoginVM p_UserLoginVM)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_UserLoginVM.LoginEmail).Data;
            if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.LoginPassword).Equals(UserObj.Password))
            {
                if (BL_DeleteUser(UserObj.UserId))
                {
                    return new TransactionResult<object>
                    {
                        Success = true,
                        RedirectURL = "/Home/Index",
                        Message = "Account Deletion successful"
                    };
                }
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something went wrong.Please try again"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Incorrect password"
                };
            }
        }

        public bool BL_CheckForEmailAvailability(string p_Email)
        {
            return new DAL_User().DAL_CheckForEmailAvailability(p_Email).Data;
        }

        public string BL_GenerateNewPassword(string p_Email)
        {
            string NewPassword = Membership.GeneratePassword(6, 1);
            bool result = new DAL_User().DAL_SaveNewPassword(p_Email, new MD5Hashing().GetMd5Hash(NewPassword));
            return NewPassword;
        }

        public TransactionResult<object> BL_ChangePassword(ChangePasswordVM p_Obj, string p_Email)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_Email).Data;
            if (new MD5Hashing().GetMd5Hash(p_Obj.OldPassword).Equals(UserObj.Password))
            {
                bool result = new DAL_User().DAL_SaveNewPassword(UserObj.Email, new MD5Hashing().GetMd5Hash(p_Obj.NewPassword));
                return new TransactionResult<object>
                {
                    Success = true,
                    RedirectURL = "/Notes/List",
                    Message = "Password Successfully updated"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Incorrect Old Password"
                };
            }
        }

        public List<CountryVM> BL_GetCountryList()
        {
            List<CountryVM> CountryList = new List<CountryVM>();
            try
            {
                foreach (var Country in new DAL_User().DAL_GetCountryList().Data.ToList())
                {
                    CountryList.Add(new CountryVM
                    {
                        CountryId = Country.CountryId,
                        CountryName = Country.CountryName
                    });
                }
                return CountryList;
            }
            catch (Exception e)
            {
                return CountryList;
            }
        }

        public TransactionResult<object> BL_SaveContactInfo(ContactVM ContactVM)
        {
            if (new DAL_User().DAL_SaveContactInfo(new CustomerContactInfo
            {
                Name = ContactVM.Name,
                Email = ContactVM.Email,
                Subject = ContactVM.Subject,
                Message = ContactVM.Message
            }).Data)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Message sent Succesfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something went wrong,Please try again"
                };
            }
        }
    }
}
