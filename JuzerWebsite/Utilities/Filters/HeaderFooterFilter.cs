using BusinessEntities.Entities.Entity_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Utilities.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult) // v will null when v is not a ViewResult
            {
                ViewResult VR = filterContext.Result as ViewResult;
                if (VR.Model is BaseViewModel)//bvm will be null when we want a view without Header and footer
                {
                    BaseViewModel bvm = VR.Model as BaseViewModel;
                    bvm.Title = "My Application";
                    bvm.FirstName = (HttpContext.Current.Session["MST_UserInfo"] as MST_UserInfo).FirstName;
                    bvm.DeveloperName = "Juzer Hakimji";
                    bvm.Year = DateTime.Now.Year.ToString();
                }
            }
        }
    }
}