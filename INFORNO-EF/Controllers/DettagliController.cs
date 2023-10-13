using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INFORNO_EF.Models;

namespace INFORNO_EF.Controllers
{
    public class DettagliController : Controller
    {
        private Context db = new Context();
        // GET: Dettagli
        public ActionResult Index(int id)
        {
            return View(db.Pizze.ToList());
        }

        public ActionResult Create(int id) 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Dettagli d) 
        {
            return View();
        }
    }
}