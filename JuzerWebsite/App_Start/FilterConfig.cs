using JuzerWebsite.Utilities.Filters;
using System.Web;
using System.Web.Mvc;

namespace JuzerWebsite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UserExceptionFilter());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
