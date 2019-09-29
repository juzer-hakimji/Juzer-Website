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

namespace JuzerWebsite.Areas.Notes.Controllers
{
    public class NotesController : Controller
    {
        private BL_Notes BLObj { get; set; }

        public NotesController()
        {
            BLObj = new BL_Notes();
        }

        public ActionResult List()
        {
            return View("Notes", new NotesVM
            {
                Title = "Notes",
                FirstName = (Session["MST_UserInfo"] as MST_UserInfo).FirstName,
                DeveloperName = "Juzer Hakimji",
                Year = DateTime.Now.Year.ToString()
            });
        }

        public JsonResult GetListData()
        {
            List<NotesVM> NotesList = BLObj.BL_GetNotesList((Session["MST_UserInfo"] as MST_UserInfo).UserId);
            return Json(NotesList, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult SaveNote(NotesVM P_NotesVM)
        {
            TransactionResult<object> result;
            if (P_NotesVM.NoteId != null)
            {
                result = BLObj.BL_UpdateNote(P_NotesVM);
            }
            else
            {
                result = BLObj.BL_SaveNote(P_NotesVM, (Session["MST_UserInfo"] as MST_UserInfo).UserId);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            TransactionResult<object> result = BLObj.BL_ChangeNoteImportance(NoteId, IsImportant);
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteNote(int p_NoteId)
        {
            TransactionResult<object> result = BLObj.BL_DeleteNote(p_NoteId);
            return Json(result);
        }
    }
}
