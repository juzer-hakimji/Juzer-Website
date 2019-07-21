using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.Business_Logic_Classes;
using JuzerWebsite.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Areas.Notes.Controllers
{
    public class NotesController : Controller
    {
        private BL_Notes BLObj { get; set; }

        public NotesController()
        {
            BLObj = new BL_Notes();
        }

        [HeaderFooterFilter]
        public ActionResult List()
        {
            return View("Notes", new BaseViewModel());
        }

        [HttpPost]
        public JsonResult GetListData()
        {
            MST_UserInfo Userobj = Session["MST_UserInfo"] as MST_UserInfo;
            List<NotesVM> NotesList= BLObj.BL_GetNotesList(Userobj.UserId);
            return Json(NotesList);
        }
    }
}