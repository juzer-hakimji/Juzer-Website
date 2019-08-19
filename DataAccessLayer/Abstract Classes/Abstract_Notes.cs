using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using DataAccessLayer.Data_Model;
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

        public DBContextResult<List<usp_GetNotesList_Result>> Select(int p_UserId)
        {
            //return db.usp_GetNotesList(p_UserId).ToList();
            return ExecuteDALMethod<int, List<usp_GetNotesList_Result>>(db, (DataContext, P_UserId) =>
            {
                return DataContext.usp_GetNotesList(P_UserId).ToList();
            }, p_UserId);
        }

        public DBContextResult<object> Insert(TRN_Notes p_TRN_Notes)
        {
            return ExecuteDALMethod<TRN_Notes, object>(db, (DataContext, P_TRN_Notes) =>
             {
                 db.TRN_Notes.Add(P_TRN_Notes);
                 db.SaveChanges();
                 return null;
             }, p_TRN_Notes);
        }

        public DBContextResult<object> Update(TRN_Notes p_TRN_Notes)
        {
            return this.ExecuteDALMethod<TRN_Notes, object>(db, (DataContext, P_TRN_Notes) =>
            {
                TRN_Notes NoteObj = DataContext.TRN_Notes.Find(P_TRN_Notes.NoteId);
                NoteObj.Subject = P_TRN_Notes.Subject;
                NoteObj.NoteText = P_TRN_Notes.NoteText;
                NoteObj.CreatedDate = P_TRN_Notes.CreatedDate;
                NoteObj.ModifiedDate = P_TRN_Notes.ModifiedDate;
                NoteObj.IsImportant = P_TRN_Notes.IsImportant;
                DataContext.SaveChanges();
                return null;
            }, p_TRN_Notes);
        }

        public DBContextResult<object> Delete(int p_NoteId)
        {
            return ExecuteDALMethod<int, object>(db, (DataContext, P_NoteId) =>
            {
                TRN_Notes Obj = DataContext.TRN_Notes.Find(P_NoteId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return null;
            }, p_NoteId);
        }

        public DBContextResult<object> ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            return ExecuteDALMethod<int, object>(db, (DataContext, NoteID) =>
             {
                 TRN_Notes Obj = DataContext.TRN_Notes.Find(NoteID);
                 Obj.IsImportant = IsImportant;
                 DataContext.SaveChanges();
                 return null;
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
