using BusinessEntities.Entities.Entity_Model;
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
using System.Web.Security;
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

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Save(UserDetailsVM p_UserVM)
        {
            if (ModelState.IsValid)
            {
                TransactionResult<MST_UserInfo> result = BLUser.BL_SaveUser(p_UserVM);
                if (result.Data != null)
                {
                    FormsAuthentication.SetAuthCookie(result.Data.Email, false);
                    Session["IsAdmin"] = false;
                    Session["MST_UserInfo"] = result.Data;
                    return Json(new TransactionResult<object>
                    {
                        Success = true,
                        RedirectURL = "/Notes/List",
                        Message = "Registration Successful"
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
                    Message = "Entered Details are not correct,please try again."
                });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult SendResetPasswordEmail(string p_Email)
        {
            if (ModelState.IsValid)
            {
                if (BLUser.BL_CheckForEmailAvailability(p_Email))
                {
                    string NewPassword = BLUser.BL_GenerateNewPassword(p_Email);
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("hakimjuzer@gmail.com", "9168511453")
                    };
                    using (var message = new MailMessage(new MailAddress("hakimjuzer@gmail.com", "Juzer"), new MailAddress(p_Email, "user"))
                    {
                        Subject = "Reset Password Request",
                        Body = "Hello \n\nyour new password is : " + NewPassword + "\n\n\nThank you"
                    })
                    {
                        smtp.Send(message);
                    }
                    return Json(new TransactionResult<object>
                    {
                        Success = true,
                        RedirectURL = "/Home/Index",
                        Message = "New Password has been sent to your Email Address,Please use new password to login"
                    });
                }
                else
                {
                    return Json(new TransactionResult<object>
                    {
                        Success = false,
                        Message = "email address does not exist"
                    });
                }
            }
            else
            {
                return Json(new TransactionResult<object>
                {
                    Success = false,
                    Message = "please enter correct email address"
                });
            }
        }

        [Route("User/Delete")]
        [HeaderFooterFilter]
        [HttpGet]
        public ActionResult DeleteAccount()
        {
            return View("DeleteAccount", new BaseViewModel());
        }

        [HttpGet]
        [HeaderFooterFilter]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword", new ChangePasswordVM());
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM p_Obj)
        {
            TransactionResult<object> result = BLUser.BL_ChangePassword(p_Obj, (Session["MST_UserInfo"] as MST_UserInfo).Email);
            return Json(result);
        }
    }
}

