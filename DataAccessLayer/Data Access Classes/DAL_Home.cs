using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Home: Abstract_Home
    {
        public void DAL_GetDataForComparison(int p_UserId)
        {

        }

        public List<TRN_Notes> DAL_GetImportantNotes(int p_UserId)
        {
            return this.GetImportantNotes(p_UserId);
        }
    }
}
