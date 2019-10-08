using System.Web.Mvc;

namespace JuzerWebsite.Areas.Spending
{
    public class SpendingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Spending";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Spending_default",
                "Spending/{action}/{id}",
                new { controller = "Spending", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}