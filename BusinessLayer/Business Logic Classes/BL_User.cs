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

        public MST_UserInfo BL_SaveUser(UserDetailsVM p_UserVM)
        {
            UserObj = new MST_UserInfo
            {
                FirstName = p_UserVM.FirstName,
                LastName = p_UserVM.LastName,
                CountryId = p_UserVM.CountryId,
                Email = p_UserVM.SignUpEmail,
                Password = new MD5Hashing().GetMd5Hash(p_UserVM.SignUpPassword),
                IsActive = true,
                IsAdmin = false,
                CreatedDate = DateTime.UtcNow
            };
            return IUserObj.Insert(UserObj).Data;
        }

        public bool BL_UpdateUser(UserDetailsVM p_UserVM)
        {
            UserObj = new MST_UserInfo
            {
                FirstName = p_UserVM.FirstName,
                LastName = p_UserVM.LastName,
                CountryId = p_UserVM.CountryId,
                Email = p_UserVM.SignUpEmail
            };
            return IUserObj.Update(UserObj).TransactionResult;
        }

        public bool BL_DeleteUser(int p_UserId)
        {
            return IUserObj.Delete(p_UserId).TransactionResult;
        }

        public MST_UserInfo BL_GetUserValidity(UserLoginVM p_UserLoginVM)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_UserLoginVM.LoginEmail).Data;
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
            return UserObj;
        }

        public TransactionResult BL_ValidatePassword(UserLoginVM p_UserLoginVM)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_UserLoginVM.LoginEmail).Data;
            if (new MD5Hashing().GetMd5Hash(p_UserLoginVM.LoginPassword).Equals(UserObj.Password))
            {
                if (BL_DeleteUser(UserObj.UserId))
                {
                    return new TransactionResult
                    {
                        Success = true,
                        RedirectURL = "/Home/Index",
                        Message = "User Deletion successful"
                    };
                }
                return new TransactionResult
                {
                    Success = false,
                    Message = "Something went wrong.Please try again"
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Something went wrong.Please try again"
                };
            }
        }

        public bool BL_CheckForEmailAvailability(string p_Email)
        {
            return new DAL_User().DAL_CheckForEmailAvailability(p_Email).TransactionResult;
        }

        public string BL_GenerateNewPassword(int p_UserId)
        {
            string NewPassword = Membership.GeneratePassword(6, 1);
            bool result = new DAL_User().DAL_SaveNewPassword(p_UserId, new MD5Hashing().GetMd5Hash(NewPassword));
            return NewPassword;
        }

        public TransactionResult BL_ChangePassword(ChangePasswordVM p_Obj, string p_Email)
        {
            MST_UserInfo UserObj = new DAL_User().DAL_GetUserValidity(p_Email).Data;
            if (new MD5Hashing().GetMd5Hash(p_Obj.OldPassword).Equals(UserObj.Password))
            {
                bool result = new DAL_User().DAL_SaveNewPassword(UserObj.UserId, new MD5Hashing().GetMd5Hash(p_Obj.NewPassword));
                return new TransactionResult
                {
                    Success = true,
                    RedirectURL = "/Notes/List",
                    Message = "Password Successfully updated"
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Incorrect Old Password"
                };
            }
        }

        public List<CountryVM> BL_GetCountryList()
        {
            List<CountryVM> CountryList = new List<CountryVM>();
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
    }
}
