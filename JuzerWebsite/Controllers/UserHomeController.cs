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
    public class UserHomeController : Controller
    {
        private BL_Home BLObj { get; set; }

        public UserHomeController()
        {
            BLObj = new BL_Home();
        }

        public ActionResult Summary()
        {
            return View("UserHome", new UserHomeVM
            {
                Title = "Home",
                FirstName = (Session["MST_UserInfo"] as MST_UserInfo).FirstName,
                DeveloperName = "Juzer Hakimji",
                Year = DateTime.Now.Year.ToString()
            });
        }

        public JsonResult GetListData()
        {
            List<NotesVM> ImpNotesList = BLObj.BL_GetImportantNotes((Session["MST_UserInfo"] as MST_UserInfo).UserId);
            return Json(ImpNotesList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGraphData()
        {
            UserHomeVM UserHomeVM = BLObj.BL_GetDataForComparison((Session["MST_UserInfo"] as MST_UserInfo).UserId);
            return Json(UserHomeVM, JsonRequestBehavior.AllowGet);
        }
    }
}
