using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Error : BaseDAL
    {
        private JuzerWebsiteEntities db;

        public DAL_Error()
        {
            db = new JuzerWebsiteEntities();
        }

        public bool DAL_LogExceptionToDataBase(WebsiteErrorLog p_WebsiteErrorLog)
        {
            return this.ExecuteDALMethod<WebsiteErrorLog, bool>(db, (DataContext, P_WebsiteErrorLog) =>
            {
                DataContext.WebsiteErrorLogs.Add(P_WebsiteErrorLog);
                DataContext.SaveChanges();
                return true;
            }, p_WebsiteErrorLog);
        }
    }
}
