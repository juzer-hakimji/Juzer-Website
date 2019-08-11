using BusinessLayer.Business_Logic_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Controllers
{
    public class AnalysisController : Controller
    {
        private BL_Analysis BLAnalysis { get; set; }

        public AnalysisController()
        {
            BLAnalysis = new BL_Analysis();
        }

        public ActionResult Index()
        {
            AnalysisVM AnalysisVM = new AnalysisVM();
            AnalysisVM.RemoveAdminList = BLAnalysis.BL_GetAddOrRemoveAdminList(false);
            return View("Analysis", AnalysisVM);
        }

        public ActionResult AddOrRemoveAdmin(string UserIds,bool IsAdmin)
        {
            bool Result = BLAnalysis.BL_AddOrRemoveAdmin(UserIds, IsAdmin);
            return Json(new { Result });
        }

        public ActionResult GetAddAdminList()
        {
            return Json(BLAnalysis.BL_GetAddOrRemoveAdminList(true));
        }
    }
}