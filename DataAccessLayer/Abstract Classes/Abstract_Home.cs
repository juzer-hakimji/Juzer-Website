using BusinessEntities.Entities.Entity_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Home : IDisposable
    {
        JuzerWebsiteEntities db;

        public Abstract_Home()
        {
            db = new JuzerWebsiteEntities();
        }

        protected void GetDataForComparison(int p_UserId)
        {
            //get total of incomes and total of expenses for current month and year using stored procedure
        }

        protected List<TRN_Notes> GetImportantNotes(int p_UserId)
        {
            List<TRN_Notes> List = db.TRN_Notes.Where(x =>x.UserId == p_UserId && x.IsImportant == true && x.IsActive != false ).ToList();
            return List;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        ~Abstract_Home()
        {
            Dispose();
        }
    }
}
