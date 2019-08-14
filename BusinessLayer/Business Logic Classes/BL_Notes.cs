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
            List<usp_GetNotesList_Result> NotesList = INotesObj.Select(p_UserId).Data;
            foreach (usp_GetNotesList_Result Note in NotesList)
            {
                NotesVM NotesObj = new NotesVM
                {
                    NoteId = Note.NoteId,
                    Subject = Note.Subject,
                    CreatedDate = Note.CreatedDate.ToString(),
                    NoteText = Note.NoteText,
                    IsImportant = Note.IsImportant
                };
                NotesVMList.Add(NotesObj);
            }
            return NotesVMList;
        }

        public TransactionResult BL_SaveNote(NotesVM p_NotesVM)
        {
            NoteObj = new TRN_Notes
            {
                Subject = p_NotesVM.Subject,
                CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
                NoteText = p_NotesVM.NoteText,
                IsActive = true,
                IsImportant = false
            };
            if (INotesObj.Insert(NoteObj).TransactionResult)
            {
                return new TransactionResult{
                    Success = true,
                    Message = "Note Saved Successfully"
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult BL_UpdateNote(NotesVM p_NotesVM)
        {
            NoteObj = new TRN_Notes
            {
                NoteId = p_NotesVM.NoteId ?? 0,
                Subject = p_NotesVM.Subject,
                CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
                NoteText = p_NotesVM.NoteText
            };
            if (INotesObj.Update(NoteObj).TransactionResult)
            {
                return new TransactionResult
                {
                    Success = true,
                    Message = "Note Updated Successfully"
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult BL_DeleteNote(int p_NoteId)
        {
            if (INotesObj.Delete(p_NoteId).TransactionResult)
            {
                return new TransactionResult
                {
                    Success = true,
                    Message = "Note Deletion Successful"
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult BL_ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            if (new DAL_Notes().DAL_ChangeNoteImportance(NoteId, IsImportant).TransactionResult)
            {
                return new TransactionResult
                {
                    Success = true,
                    Message = "Note Updation Successful"
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }
    }
}
