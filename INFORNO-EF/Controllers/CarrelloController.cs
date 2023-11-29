using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFORNO_EF.Controllers
{
    public class CarrelloController : Controller
    {
        // GET: Carrello
        public ActionResult Index()
        {
            return View();
        }
    }
}