using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Notes : IBasicOperationsNotes , IDisposable
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
            db.TRN_Notes.Add(p_TRN_Notes);
            db.SaveChanges();
            return true;
        }

        public bool Update(TRN_Notes p_TRN_Notes)
        {
            TRN_Notes Obj = db.TRN_Notes.Find(p_TRN_Notes.NoteId);
            Obj.Subject = p_TRN_Notes.Subject;
            Obj.NoteText = p_TRN_Notes.NoteText;
            Obj.CreatedDate = p_TRN_Notes.CreatedDate;
            Obj.ModifiedDate = p_TRN_Notes.ModifiedDate;
            Obj.IsImportant = p_TRN_Notes.IsImportant;
            db.SaveChanges();
            return true;
        }

        public bool Delete(int p_NoteId)
        {
            TRN_Notes Obj = db.TRN_Notes.Find(p_NoteId);
            Obj.IsActive = false;
            db.SaveChanges();
            return true;
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
