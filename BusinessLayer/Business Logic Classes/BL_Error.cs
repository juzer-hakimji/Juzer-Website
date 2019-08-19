using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Error
    {
        public DAL_Error DALObj { get; set; }

        public BL_Error()
        {
            DALObj = new DAL_Error();
        }

        public void BL_LogExceptionToDataBase(Exception ex)
        {
            DALObj.DAL_LogExceptionToDataBase(new WebsiteErrorLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                MethodName = ex.TargetSite.Name,
                TypeName = ex.GetType().ToString(),
                //Indian time is 5 hours and 30 minutes ahead of utc time
                //Date = DateTime.UtcNow.Date,
                //Time = DateTime.UtcNow.TimeOfDay
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay
            });
        }
    }
}
