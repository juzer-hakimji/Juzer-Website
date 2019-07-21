using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult VR = filterContext.Result as ViewResult;
            if (VR != null) // v will null when v is not a ViewResult
            {
                BaseViewModel bvm = VR.Model as BaseViewModel;
                if (bvm != null)//bvm will be null when we want a view without Header and footer
                {
                    bvm.Title = "My Application";
                    bvm.FirstName = HttpContext.Current.Session["FirstName"].ToString();
                    bvm.DeveloperName = "Juzer Hakimji";//Can be set to dynamic value
                    bvm.Year = DateTime.Now.Year.ToString();
                }
            }
        }
    }
}