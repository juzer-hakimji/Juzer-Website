using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.Business_Logic_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Controllers
{
    public class UserController : Controller
    {
        private BL_User BLUser { get; set; }

        UserController()
        {
            BLUser = new BL_User();
        }
        // GET: User
        public ActionResult Save(UserDetailsVM p_UserVM)
        {
            if (ModelState.IsValid)
            {
                MST_UserInfo MST_UserInfo = BLUser.BL_SaveUser(p_UserVM);
                Session["MST_UserInfo"] = MST_UserInfo;
                return RedirectToAction("Notes", "List");
            }
            else
            {
                return Json( new { result = false } );
            }
        }
    }
}
