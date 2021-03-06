﻿using System.Web.Mvc;

namespace JuzerWebsite.Areas.Notes
{
    public class NotesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Notes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Notes_default",
                "Notes/{action}/{id}",
                new { Controller = "Notes",action = "List", id = UrlParameter.Optional }
            );
        }
    }
}