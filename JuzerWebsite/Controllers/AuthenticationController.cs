using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.Business_Logic_Classes;
using BusinessLayer.TransactionResultModel;
using BusinessLayer.User_Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModel;

namespace JuzerWebsite.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private BL_User BLUser { get; set; }

        public AuthenticationController()
        {
            BLUser = new BL_User();
        }

        //This method will be called when user user enters information and clicks login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginVM p_UserLoginVM)
        {
            if (ModelState.IsValid)
            {
                bool IsAdmin = false;
                MST_UserInfo MST_UserInfo = BLUser.BL_GetUserValidity(p_UserLoginVM);
                if (MST_UserInfo.UserStatus == MST_UserInfo.EnumUserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (MST_UserInfo.UserStatus == MST_UserInfo.EnumUserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return Json(new { result = false });
                    //return RedirectToAction("Index","Home");
                }
                FormsAuthentication.SetAuthCookie(p_UserLoginVM.LoginEmail, false);
                Session["IsAdmin"] = IsAdmin;
                Session["MST_UserInfo"] = MST_UserInfo;
                return Json(new TransactionResult
                {
                    Success = true,
                    RedirectURL = Url.Action("List","Notes")
                });
            }
            else
            {
                return Json(new TransactionResult
                {
                    Success = false,
                    Message = "Invalid Username or Password"
                });
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPut]
        public ActionResult ValidatePassword(string p_Password)
        {
            bool IsUserExists = BLUser.BL_ValidatePassword(new UserLoginVM { LoginEmail = (Session["MST_UserInfo"] as MST_UserInfo).Email, LoginPassword = p_Password });
            return Json(new { IsUserExists });
        }
    }
}