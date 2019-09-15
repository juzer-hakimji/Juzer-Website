using BusinessEntities.Entities.Entity_Model;
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

        public ActionResult Index()
        {
            return View("Analysis", new AnalysisVM
            {
                Title = "Analysis",
                FirstName = (Session["MST_UserInfo"] as MST_UserInfo).FirstName,
                DeveloperName = "Juzer Hakimji",
                Year = DateTime.Now.Year.ToString(),
                RemoveAdminList = BLAnalysis.BL_GetAddOrRemoveAdminList(false)
            });
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrRemoveAdmin(string UserIds, bool IsAdmin)
        {
            TransactionResult<object> Result = BLAnalysis.BL_AddOrRemoveAdmin(UserIds, IsAdmin);
            return Json(Result);
        }

        public ActionResult GetAddAdminList()
        {
            List<object> Adminlst = new List<object>();
            BLAnalysis.BL_GetAddOrRemoveAdminList(true).ForEach(x => Adminlst.Add(new
            {
                id = x.UserId,
                text = x.SignUpEmail
            }));
            return Json(Adminlst, JsonRequestBehavior.AllowGet);
        }
    }
}
