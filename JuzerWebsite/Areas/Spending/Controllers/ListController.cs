using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuzerWebsite.Areas.Spending.Controllers
{
    public class ListController : Controller
    {
        // GET: Spending/List
        public ActionResult Index()
        {
            return View();
        }
    }
}