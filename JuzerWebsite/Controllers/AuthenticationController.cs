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
        public ActionResult Login(UserLoginVM p_UserLoginVM)
        {
            if (ModelState.IsValid)
            {
                UserStatus status = BLUser.BL_GetUserValidity(p_UserLoginVM);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(p_UserLoginVM.Email, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}