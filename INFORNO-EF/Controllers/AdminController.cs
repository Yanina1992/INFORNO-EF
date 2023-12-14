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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pizze p, string Nome, HttpPostedFileBase FileFoto, string Prezzo, string TempoConsegna, string Ingredienti)
        {
            if (ModelState.IsValid)
            {
                if (FileFoto.ContentLength > 0)
                {
                    string nomeFoto = FileFoto.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Immagini"), nomeFoto);
                    FileFoto.SaveAs(pathToSave);
                }

                p.Foto = FileFoto.FileName;
                p.Nome= Nome;
                p.Prezzo= Prezzo;
                p.TempoConsegna= TempoConsegna;
                p.Ingredienti= Ingredienti;
                db.Pizze.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(p);
        }

        public ActionResult GestisciOrdiniConclusi()
        {
            //var ordiniList = TempData["ordiniList"];
            var ordiniList = db.Ordini.Where(m => m.Concluso == true).ToList();
            return View(ordiniList);
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