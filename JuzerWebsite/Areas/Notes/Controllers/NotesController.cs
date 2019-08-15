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

        [HeaderFooterFilter]
        public ActionResult List()
        {
            return View("Notes", new BaseViewModel());
        }

        [HttpPost]
        public JsonResult GetListData()
        {
            MST_UserInfo Userobj = Session["MST_UserInfo"] as MST_UserInfo;
            List<NotesVM> NotesList = BLObj.BL_GetNotesList(Userobj.UserId);
            return Json(NotesList);
        }

        [Route("Notes/Save")]
        [HttpPost]
        public JsonResult SaveNote(NotesVM P_NotesVM)
        {
            TransactionResult result;
            if (P_NotesVM.NoteId != null)
            {
                result = BLObj.BL_UpdateNote(P_NotesVM);
            }
            else
            {
                result = BLObj.BL_SaveNote(P_NotesVM);
            }
            return Json(new { result });
        }

        [HttpPut]
        public JsonResult ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            TransactionResult result = BLObj.BL_ChangeNoteImportance(NoteId, IsImportant);
            return Json(new { result });
        }

        [Route("Notes/Delete")]
        [HttpPut]
        public JsonResult DeleteNote(int p_NoteId)
        {
            TransactionResult result = BLObj.BL_DeleteNote(p_NoteId);
            return Json(new { result });
        }
    }
}
