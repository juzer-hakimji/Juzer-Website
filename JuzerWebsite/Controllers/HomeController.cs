using BusinessLayer.Business_Logic_Classes;
using BusinessLayer.TransactionResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new MainUserVM
            {
                UserDetailsVM = new UserDetailsVM
                {
                    CountryList = new BL_User().BL_GetCountryList()
                },
                UserLoginVM = new UserLoginVM(),
                ResetPasswordVM = new ResetPasswordVM(),
                ContactVM = new ContactVM()
            });
        }

        [HttpPost]
        public ActionResult ContactMessage(ContactVM ContactVM)
        {
            TransactionResult<object> result = new BL_User().BL_SaveContactInfo(ContactVM);
            return Json(result);
        }

        public FileResult GetTermsAndConditions()
        {
            return File("~/Content/Documents/Resume.pdf", "application/pdf");
        }
    }
}
