using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INFORNO_EF.Models;
using System.Net;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace INFORNO_EF.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Models.Context db = new Models.Context();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(db.Pizze.ToList());
        }
        
        //Pizza details for costumers
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizza = db.Pizze.Find(id);
            if(pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }
    }
}
