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
    public class DAL_Home: Abstract_Home
    {
        public DBContextResult<List<usp_GetIESummary_Result>> DAL_GetDataForComparison(int p_UserId)
        {
            return GetDataForComparison(p_UserId);
        }

        public DBContextResult<List<TRN_Notes>> DAL_GetImportantNotes(int p_UserId)
        {
            return GetImportantNotes(p_UserId);
        }
    }
}
