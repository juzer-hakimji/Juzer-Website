using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Notes : BaseDAL, IBasicOperationsNotes, IDisposable
    {
        private JuzerWebsiteEntities db;

        public Abstract_Notes()
        {
            db = new JuzerWebsiteEntities();
        }

        public List<usp_GetNotesList_Result> Select(int p_UserId)
        {
            return db.usp_GetNotesList(p_UserId).ToList();
        }

        public bool Insert(TRN_Notes p_TRN_Notes)
        {
            return this.ExecuteDALMethod<TRN_Notes, bool>(db, (DataContext, P_TRN_Notes) =>
             {
                 db.TRN_Notes.Add(P_TRN_Notes);
                 db.SaveChanges();
                 return true;
             }, p_TRN_Notes);
        }

        public bool Update(TRN_Notes p_TRN_Notes)
        {
            return this.ExecuteDALMethod<TRN_Notes, bool>(db, (DataContext, P_TRN_Notes) =>
            {
                TRN_Notes NoteObj = DataContext.TRN_Notes.Find(P_TRN_Notes.NoteId);
                NoteObj.Subject = P_TRN_Notes.Subject;
                NoteObj.NoteText = P_TRN_Notes.NoteText;
                NoteObj.CreatedDate = P_TRN_Notes.CreatedDate;
                NoteObj.ModifiedDate = P_TRN_Notes.ModifiedDate;
                NoteObj.IsImportant = P_TRN_Notes.IsImportant;
                DataContext.SaveChanges();
                return true;
            }, p_TRN_Notes);
        }

        public bool Delete(int p_NoteId)
        {
            //TRN_Notes NoteObj = new TRN_Notes
            //{
            //    NoteId = p_NoteId
            //};
            return this.ExecuteDALMethod<int, bool>(db, (DataContext, P_NoteId) =>
            {
                TRN_Notes Obj = DataContext.TRN_Notes.Find(P_NoteId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, p_NoteId);
        }

        public bool ChangeNoteImportance(int NoteId, bool IsImportant)
        {
                return this.ExecuteDALMethod<int, bool>(db, (DataContext, NoteID) =>
                 {
                     TRN_Notes Obj = DataContext.TRN_Notes.Find(NoteID);
                     Obj.IsImportant = IsImportant;
                     DataContext.SaveChanges();
                     return true;
                 }, NoteId);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        ~Abstract_Notes()
        {
            Dispose();
        }
    }
}
