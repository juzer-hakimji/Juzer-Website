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
    public abstract class Abstract_Notes : BaseDAL , IBasicOperationsNotes , IDisposable
    {
        JuzerWebsiteEntities db;

        public Abstract_Notes()
        {
            db = new JuzerWebsiteEntities();
        }

        public List<usp_GetNotesList_Result> Select(int p_Id)
        {
            return db.usp_GetNotesList(p_Id).ToList();
        }

        public bool Insert(TRN_Notes p_TRN_Notes)
        {
            return this.ExecuteDALMethod<TRN_Notes>(db, (DataContext, P_TRN_Notes) =>
            {
                db.TRN_Notes.Add(P_TRN_Notes);
                db.SaveChanges();
                return true;
            }, p_TRN_Notes);

            
        }

        public bool Update(TRN_Notes p_TRN_Notes)
        {
            return this.ExecuteDALMethod<TRN_Notes>(db, (DataContext, P_TRN_Notes) =>
            {
                TRN_Notes Obj = DataContext.TRN_Notes.Find(P_TRN_Notes.NoteId);
                Obj.Subject = P_TRN_Notes.Subject;
                Obj.NoteText = P_TRN_Notes.NoteText;
                Obj.CreatedDate = P_TRN_Notes.CreatedDate;
                Obj.ModifiedDate = P_TRN_Notes.ModifiedDate;
                Obj.IsImportant = P_TRN_Notes.IsImportant;
                DataContext.SaveChanges();
                return true;
            }, p_TRN_Notes);
        }

        public bool Delete(int p_NoteId)
        {
            TRN_Notes TRN_Notes = new TRN_Notes();
            TRN_Notes.NoteId = p_NoteId;
            return this.ExecuteDALMethod<TRN_Notes>(db, (DataContext, P_TRN_Notes) =>
            {
                TRN_Notes Obj = DataContext.TRN_Notes.Find(P_TRN_Notes.NoteId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, TRN_Notes);
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
