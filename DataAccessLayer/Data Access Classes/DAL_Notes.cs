using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using DataAccessLayer.Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Notes : Abstract_Notes
    {
        public DBContextResult<object> DAL_ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            return ChangeNoteImportance(NoteId, IsImportant);
        }
}
