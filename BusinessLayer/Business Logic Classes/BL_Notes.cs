using BusinessEntities.Entities.Entity_Model;
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

        public bool BL_SaveNote(NotesVM p_NotesVM)
        {
            NoteObj = new TRN_Notes
            {
                Subject = p_NotesVM.Subject,
                CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
                NoteText = p_NotesVM.NoteText,
                IsActive = true,
                IsImportant = false
            };
            return INotesObj.Insert(NoteObj).TransactionResult;
        }

        public bool BL_UpdateNote(NotesVM p_NotesVM)
        {
            NoteObj = new TRN_Notes
            {
                NoteId = p_NotesVM.NoteId ?? 0,
                Subject = p_NotesVM.Subject,
                CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture),
                NoteText = p_NotesVM.NoteText
            };
            return INotesObj.Update(NoteObj).TransactionResult;
        }

        public bool BL_DeleteNote(int p_NoteId)
        {
            return INotesObj.Delete(p_NoteId).TransactionResult;
        }

        public bool BL_ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            return new DAL_Notes().DAL_ChangeNoteImportance(NoteId, IsImportant).TransactionResult;
        }
    }
}
