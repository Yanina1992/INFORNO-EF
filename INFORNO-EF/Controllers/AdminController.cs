using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INFORNO_EF.Models;

namespace INFORNO_EF.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GestisciOrdiniConclusi()
        {
            var ordiniList = db.Ordini.Where(m => m.Concluso == true).ToList();
            return View(ordiniList);
        }
        public JsonResult GetOrdiniEvasi()
        {
            var ordiniEvasi = db.Ordini.Where(m => m.Evaso == true).Count();
            return Json(ordiniEvasi, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotaleIncassato()
        {
            var ordiniEvasi = db.Ordini.Where(m => m.Evaso == true).Sum(m => m.ImportoTotale);
            return Json(ordiniEvasi, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKUtente = new SelectList(db.Utenti, "IdUtente", "Username", ordini.FKUtente);
            return View(ordini);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdine,Data,IndirizzoSpedizione,Note,FKUtente,ImportoTotale,Concluso,Evaso")] Ordini ordini)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            ViewBag.FKUtente = new SelectList(db.Utenti, "IdUtente", "Username", ordini.FKUtente);
            return View();
        }

       
    }
}