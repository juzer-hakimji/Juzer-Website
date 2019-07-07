using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Home
    {
        private DAL_Home DALObj { get; set; }

        BL_Home()
        {
            DALObj = new DAL_Home();
        }

        public void BL_GetDataForComparison(int p_UserId)
        {

        }

        public List<NotesVM> BL_GetImportantNotes(int p_UserId)
        {
            List<NotesVM> NotesVMList = new List<NotesVM>();
            List<TRN_Notes> NotesList = DALObj.DAL_GetImportantNotes(p_UserId);
            foreach (TRN_Notes Note in NotesList)
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
    }
}
