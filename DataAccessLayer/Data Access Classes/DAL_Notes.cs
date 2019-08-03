using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Notes : Abstract_Notes
    {
        public bool DAL_ChangeNoteImportance(int NoteId, bool IsImportant)
        {
            return this.ChangeNoteImportance(NoteId, IsImportant);
        }
}
