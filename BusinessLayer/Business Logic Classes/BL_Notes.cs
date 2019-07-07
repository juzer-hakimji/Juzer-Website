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

        BL_Notes()
        {
            INotesObj = new DAL_Notes();
        }

        public List<NotesVM> BL_GetNotesList(int p_UserId)
        {
            List<NotesVM> NotesVMList = new List<NotesVM>();
            List<usp_GetNotesList_Result> NotesList = INotesObj.Select(p_UserId);
            foreach (usp_GetNotesList_Result Note in NotesList)
            {
                NotesVM NotesObj = new NotesVM();
                NotesObj.NoteId = Note.NoteId;
                NotesObj.Subject = Note.Subject;
                NotesObj.CreatedDate = Note.CreatedDate.ToString();
                NotesObj.NoteText = Note.NoteText;
                NotesVMList.Add(NotesObj);
            }
            return NotesVMList;
        }

        public bool BL_SaveNote(NotesVM p_NotesVM)
        {
            NoteObj = new TRN_Notes();
            NoteObj.Subject = p_NotesVM.Subject;
            NoteObj.CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture);
            NoteObj.NoteText = p_NotesVM.NoteText;
            NoteObj.IsActive = true;
            NoteObj.IsImportant = false;
            return INotesObj.Insert(NoteObj);
        }

        public bool BL_UpdateNote(NotesVM p_NotesVM)
        {
            NoteObj = new TRN_Notes();
            NoteObj.NoteId = p_NotesVM.NoteId;
            NoteObj.Subject = p_NotesVM.Subject;
            NoteObj.CreatedDate = Convert.ToDateTime(p_NotesVM.CreatedDate, CultureInfo.InvariantCulture);
            NoteObj.NoteText = p_NotesVM.NoteText;
            return INotesObj.Update(NoteObj);
        }

        public bool BL_DeleteNote(int p_NoteId)
        {
            return INotesObj.Delete(p_NoteId);
        }

        public bool BL_MarkNoteImportant()
        {
            return true;
        }
    }
}
