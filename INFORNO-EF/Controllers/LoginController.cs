using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using INFORNO_EF.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;


namespace INFORNO_EF.Controllers
{
    public class LoginController : Controller
    {
        private Context db = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Utenti ut)
        {
            if (ModelState.IsValid)
            {
                Utenti utente = db.Utenti.Where(u => u.Username == ut.Username && u.Password == ut.Password).FirstOrDefault();
                FormsAuthentication.SetAuthCookie(utente.Username, false);
                db.SaveChanges();

                Session["NomeCliente"] = utente.Username;

                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        public ActionResult Registrati() {  return View(); }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Registrati(Utenti utente)
        {
            if (ModelState.IsValid)
            {
                db.Utenti.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(utente);
        }

    }
}