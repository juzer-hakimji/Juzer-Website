using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Database_Utilities
{
    public class LogError
    {
        public LogError(Exception ex)
        {
            new DAL_Error().DAL_LogExceptionToDataBase(new WebsiteErrorLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                MethodName = ex.TargetSite.Name,
                TypeName = ex.GetType().ToString(),
                Date = DateTime.UtcNow.Date,
                Time = DateTime.UtcNow.TimeOfDay
            });
        }
    }
}
