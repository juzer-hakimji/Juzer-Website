﻿using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.Business_Logic_Classes;
using BusinessLayer.TransactionResultModel;
using JuzerWebsite.Utilities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Controllers
{
    public class UserController : Controller
    {
        private BL_User BLUser { get; set; }

        public UserController()
        {
            BLUser = new BL_User();
        }
        // GET: User
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserDetailsVM p_UserVM)
        {
            if (ModelState.IsValid)
            {
                MST_UserInfo MST_UserInfo = BLUser.BL_SaveUser(p_UserVM);
                Session["MST_UserInfo"] = MST_UserInfo;
                return Json(new TransactionResult
                {
                    Success = true,
                    RedirectURL = Url.Action("List", "Notes")
                });
            }
            else
            {
                return Json(new TransactionResult
                {
                    Success = false,
                    Message = "Something went wrong,Note could not be saved."
                });
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult SendResetPasswordEmail(string p_Email)
        {
            if (ModelState.IsValid)
            {
                if (BLUser.BL_CheckForEmailAvailability(p_Email))
                {
                    string NewPassword = BLUser.BL_GenerateNewPassword((Session["MST_UserInfo"] as MST_UserInfo).UserId);
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("hakimjuzer@gmail.com", "9168511453")
                    };
                    using (var message = new MailMessage(new MailAddress("hakimjuzer@gmail.com", "Juzer"), new MailAddress(p_Email, (Session["MST_UserInfo"] as MST_UserInfo).FirstName))
                    {
                        Subject = "Reset Password Request",
                        Body = "Hello \nyour new password is" + NewPassword + "\nThank you"
                    })
                    {
                        smtp.Send(message);
                    }
                    return Json(new { result = true, Message = "New Password has been sent to your Email Address" });
                }
                else
                {
                    return Json(new { result = false,Message = "email address does not exist" });
                }
            }
            else
            {
                return Json(new { result = false, Message = "please enter correct email address" });
            }
        }

        [Route("User/Delete")]
        [HeaderFooterFilter]
        [HttpGet]
        public ActionResult DeleteAccount()
        {
            return View("DeleteAccount",new BaseViewModel());
        }

        [HttpGet]
        [HeaderFooterFilter]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword",new ChangePasswordVM());
        }

        [HttpPut]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM p_Obj)
        {
            TransactionResult result = BLUser.BL_ChangePassword(p_Obj, (Session["MST_UserInfo"] as MST_UserInfo).Email);
            return Json(result);
        }
    }
}

