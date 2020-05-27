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

        [HttpPost]
        public ActionResult Login(UserLoginVM p_UserLoginVM)
        {
            if (ModelState.IsValid)
            {
                bool IsAdmin = false;
                TransactionResult<MST_UserInfo> result = BLUser.BL_GetUserValidity(p_UserLoginVM);
                if (result.Success)
                {
                    if (result.Data.UserStatus == MST_UserInfo.EnumUserStatus.AuthenticatedAdmin)
                    {
                        IsAdmin = true;
                    }
                    else if (result.Data.UserStatus == MST_UserInfo.EnumUserStatus.AuthenticatedUser)
                    {
                        IsAdmin = false;
                    }
                    else
                    {
                        ModelState.AddModelError("CredentialError", "Invalid Password");
                        return Json(new TransactionResult<object>
                        {
                            Success = false,
                            Message = "Invalid Password"
                        });
                    }
                    FormsAuthentication.SetAuthCookie(p_UserLoginVM.LoginEmail, false);
                    Session["IsAdmin"] = IsAdmin;
                    Session["MST_UserInfo"] = result.Data;
                    return Json(new TransactionResult<object>
                    {
                        Success = true,
                        RedirectURL = Url.Action("Summary", "UserHome")
                    });
                }
                else
                {
                    return Json(result);
                }

            }
            else
            {
                return Json(new TransactionResult<object>
                {
                    Success = false,
                    Message = "Please enter valid email and password"
                });
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ValidatePasswordAndDeleteUser(string p_Password)
        {
            TransactionResult<object> result = BLUser.BL_ValidatePasswordAndDeleteUser(new UserLoginVM { LoginEmail = (Session["MST_UserInfo"] as MST_UserInfo).Email, LoginPassword = p_Password });
            if (result.Success)
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            }
            return Json(result);
        }
    }
}

