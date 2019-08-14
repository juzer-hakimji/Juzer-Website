﻿using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.Business_Logic_Classes;
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginVM p_UserLoginVM)
        {
            if (ModelState.IsValid)
            {
                MST_UserInfo MST_UserInfo = BLUser.BL_GetUserValidity(p_UserLoginVM);
                bool IsAdmin = false;
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
                FormsAuthentication.SetAuthCookie(p_UserLoginVM.Email, false);
                Session["IsAdmin"] = IsAdmin;
                Session["MST_UserInfo"] = MST_UserInfo;
                return RedirectToAction("Notes", "List");
            }
            else
            {
                return Json(new { result = false });
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult ValidatePassword(string p_Password)
        {
            bool IsUserExists = BLUser.BL_ValidatePassword(new UserLoginVM { Email = (Session["MST_UserInfo"] as MST_UserInfo).Email, Password = p_Password });
            return Json(new { IsUserExists });
        }
    }
}