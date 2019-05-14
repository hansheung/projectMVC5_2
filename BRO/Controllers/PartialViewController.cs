using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BRO.Controllers
{
    public class PartialViewController : Controller
    {
        // GET: PartialView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult viewGroup()
        {
            return View();
        }

        public ActionResult VenPrcDetEnt()
        {
            return View();
        }
    }
}