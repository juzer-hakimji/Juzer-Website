using BusinessEntities.Entities.Entity_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBasicOperationsNotes
    {
        List<usp_GetNotesList_Result> Select(int p_Id);
        bool Insert(TRN_Notes p_Obj);
        bool Update(TRN_Notes p_Obj);
        bool Delete(int p_NoteId);
    }
}
