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

        public BL_Home()
        {
            DALObj = new DAL_Home();
        }

        public UserHomeVM BL_GetDataForComparison(int p_UserId)
        {
            List<usp_GetIESummary_Result> Summarylst = DALObj.DAL_GetDataForComparison(p_UserId).Data;
            return new UserHomeVM
            {
                MonthVM = new MonthVM { MonthExpense = Summarylst[0].MonthExpense, MonthIncome = Summarylst[0].MonthIncome },
                YearVM = new YearVM { YearExpense = Summarylst[0].YearExpense, YearIncome = Summarylst[0].YearIncome }
            };
        }

        public List<NotesVM> BL_GetImportantNotes(int p_UserId)
        {
            List<NotesVM> NotesVMList = new List<NotesVM>();
            foreach (TRN_Notes Note in DALObj.DAL_GetImportantNotes(p_UserId).Data)
            {
                NotesVM NotesObj = new NotesVM
                {
                    NoteId = Note.NoteId,
                    Subject = Note.Subject,
                    CreatedDate = Note.CreatedDate.ToString(),
                    NoteText = Note.NoteText
                };
                NotesVMList.Add(NotesObj);
            }
            return NotesVMList;
        }
    }
}
