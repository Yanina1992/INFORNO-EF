using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INFORNO_EF.Models;

namespace INFORNO_EF.Controllers
{
    public class OrdiniController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.Utenti);
            return View(ordini.ToList());
        }
        public ActionResult Details(int? id)
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

            //The total amount due must be recalculated taken in account the quantity and the kind of pizza that every order detail has
            var findDetails = db.Dettagli.Where(m => m.FKOrdine == ordini.IdOrdine).ToList();

            var importoTot = 0.0m;

            foreach(Dettagli d in findDetails)
            {
                var prezzo = db.Pizze.Where(m => m.IdPizza == d.FKPizza).Select(m => new { m.Prezzo}.Prezzo).FirstOrDefault();
                var prezzoD = Convert.ToDecimal(prezzo);
                var quantita = Convert.ToDecimal(d.Quantita);

                importoTot += prezzoD * quantita;
            }

            if (ModelState.IsValid)
            {
                ordini.ImportoTotale = importoTot;
                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Details", new { @id = ordini.IdOrdine });

            }

            return View(ordini);
        }
        public ActionResult Create()
        {
            ViewBag.FKUtente = new SelectList(db.Utenti, "IdUtente", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IndirizzoSpedizione")] Ordini ordini)
        {
            ordini.Data = DateTime.Today;
            var name = User.Identity.Name.ToString();
            ordini.FKUtente = db.Utenti.Where(m => m.Username == name).FirstOrDefault().IdUtente;

            //Retrieve data from Dettagli > Create, in order to calculate the ImportoTotale by multiplicating price and quantity
            var fkPizza = Convert.ToInt32(TempData["fkPizza"]);
            var quantita = TempData["quantity"];

            var findPizza = db.Pizze.Find(fkPizza);
            var prezzo = Convert.ToDecimal(findPizza.Prezzo);

            var importo = prezzo * Convert.ToInt32(quantita);

            ordini.ImportoTotale = importo;

            if (ModelState.IsValid)
            {
                db.Ordini.Add(ordini);
                db.SaveChanges();

                //After having create the Ordine, I can create the Dettagli
                Dettagli dettagli = new Dettagli();
                dettagli.FKPizza = Convert.ToInt32(fkPizza);
                dettagli.Quantita = Convert.ToInt32(quantita);
                dettagli.FKOrdine = ordini.IdOrdine;

                if (ModelState.IsValid)
                {
                    db.Dettagli.Add(dettagli);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { @id = ordini.IdOrdine });
                }

            }

            ViewBag.FKUtente = new SelectList(db.Utenti, "IdUtente", "Username", ordini.FKUtente);
            return RedirectToAction("Details", new { @id = ordini.IdOrdine });
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
                ordini.Concluso = true;

                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.OrdineConcluso = "Ti ringraziamo per l'acquisto!";
                return View();
            }
            ViewBag.FKUtente = new SelectList(db.Utenti, "IdUtente", "Username", ordini.FKUtente);
            return View();
        }
        public ActionResult Delete(int? id)
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
            return View(ordini);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordini ordini = db.Ordini.Find(id);
            db.Ordini.Remove(ordini);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
