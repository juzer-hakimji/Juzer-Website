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
            new FileLogger().LogException(filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}