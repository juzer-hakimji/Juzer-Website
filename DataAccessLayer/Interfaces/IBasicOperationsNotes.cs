using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBasicOperationsNotes
    {
        DBContextResult<List<usp_GetNotesList_Result>> Select(int p_Id);
        DBContextResult<object> Insert(TRN_Notes p_Obj);
        DBContextResult<object> Update(TRN_Notes p_Obj);
        DBContextResult<object> Delete(int p_NoteId);
    }
}
