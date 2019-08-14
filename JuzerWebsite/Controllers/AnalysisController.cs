using BusinessLayer.Business_Logic_Classes;
using BusinessLayer.TransactionResultModel;
using JuzerWebsite.Utilities.Filters;
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

        [HeaderFooterFilter]
        public ActionResult Index()
        {
            AnalysisVM AnalysisVM = new AnalysisVM
            {
                RemoveAdminList = BLAnalysis.BL_GetAddOrRemoveAdminList(false)
            };
            return View("Analysis", AnalysisVM);
        }

        public ActionResult AddOrRemoveAdmin(string UserIds,bool IsAdmin)
        {
            TransactionResult Result = BLAnalysis.BL_AddOrRemoveAdmin(UserIds, IsAdmin);
            return Json(new { Result });
        }

        public ActionResult GetAddAdminList()
        {
            return Json(BLAnalysis.BL_GetAddOrRemoveAdminList(true));
        }
    }
}