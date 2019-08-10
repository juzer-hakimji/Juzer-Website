using BusinessLayer.Business_Logic_Classes;
using JuzerWebsite.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuzerWebsite.Utilities.Filters
{
    public class UserExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            new FileLogger().LogExceptionToFile(filterContext.Exception);
            new BL_Error().BL_LogExceptionToDataBase(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}