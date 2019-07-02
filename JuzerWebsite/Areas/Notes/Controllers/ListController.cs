using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuzerWebsite.Areas.Notes.Controllers
{
    public class ListController : Controller
    {
        // GET: Notes/List
        public ActionResult Index()
        {
            return View();
        }
    }
}