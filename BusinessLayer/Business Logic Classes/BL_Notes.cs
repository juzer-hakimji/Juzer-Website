using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.TransactionResultModel;
using DataAccessLayer.Data_Access_Classes;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Notes
    {
        private IBasicOperationsNotes INotesObj { get; set; }
        private TRN_Notes NoteObj { get; set; }

        public BL_Notes()
        {
            INotesObj = new DAL_Notes();
        }

        public List<NotesVM> BL_GetNotesList(int p_UserId)
        {
            List<NotesVM> NotesVMList = new List<NotesVM>();
            foreach (usp_GetNotesList_Result Note in INotesObj.Select(p_UserId).Data)
            {
                NotesVM NotesObj = new NotesVM
                {
                    NoteId = Note.NoteId,
                    Subject = Note.Subject,
                    CreatedDate = Note.CreatedDate.ToString("dd/MMM/yyyy"),
                    NoteText = Note.NoteText,
                    IsImportant = Note.IsImportant
                };
                NotesVMList.Add(NotesObj);
            }
            return NotesVMList;
        }

        public TransactionResult<object> BL_SaveNote(NotesVM p_NotesVM,int p_UserId)
        {
            //NoteObj = new TRN_Notes
            //{
            //    Subject = p_NotesVM.Subject,
            //    CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
            //    NoteText = p_NotesVM.NoteText,
            //    UserId = p_UserId,
            //    IsActive = true,
            //    IsImportant = false
            //};
            if (INotesObj.Insert(new TRN_Notes
            {
                Subject = p_NotesVM.Subject,
                CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
                NoteText = p_NotesVM.NoteText,
                UserId = p_UserId,
                IsActive = true,
                IsImportant = false
            }).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Note Saved Successfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_UpdateNote(NotesVM p_NotesVM)
        {
            //NoteObj = new TRN_Notes
            //{
            //    NoteId = p_NotesVM.NoteId ?? 0,
            //    Subject = p_NotesVM.Subject,
            //    CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
            //    NoteText = p_NotesVM.NoteText,
            //    ModifiedDate = DateTime.UtcNow
            //};
            if (INotesObj.Update(new TRN_Notes
            {
                NoteId = p_NotesVM.NoteId ?? 0,
                Subject = p_NotesVM.Subject,
                CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
                NoteText = p_NotesVM.NoteText,
                ModifiedDate = DateTime.UtcNow
            }).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Note Updated Successfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_DeleteNote(int p_NoteId)
        {
            if (INotesObj.Delete(p_NoteId).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Note Deletion Successful"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            if (new DAL_Notes().DAL_ChangeNoteImportance(NoteId, IsImportant).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Note Updation Successful"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }
    }
}
