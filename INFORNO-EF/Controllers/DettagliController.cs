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
    public class DettagliController : Controller
    {
        private Context db = new Context();

        //private Context db = new Context();
        //// GET: Dettagli

        //List<Dettagli> carrello = new List<Dettagli>();
        //public ActionResult Index(int id)
        //{
        //    return View(db.Pizze.ToList());
        //}

        //public ActionResult Create(int id, int? quantita)
        //{
        //    Dettagli dettagli = new Dettagli();
        //    dettagli.Quantita = quantita;
        //    dettagli.FKPizza = id;

        //    carrello.Add(dettagli);
        //    Session["carrello"] = carrello;


        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Dettagli d)
        //{
        //    return View();
        //}

        // GET: Dettagli
        public ActionResult Index()
        {
            var dettagli = db.Dettagli.Include(d => d.Ordini).Include(d => d.Pizze);
            return View(dettagli.ToList());
        }

        // GET: Dettagli/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dettagli dettagli = db.Dettagli.Find(id);
            if (dettagli == null)
            {
                return HttpNotFound();
            }
            return View(dettagli);
        }

        // GET: Dettagli/Create
        public ActionResult Create()
        {
            ViewBag.FKOrdine = new SelectList(db.Ordini, "IdOrdine", "IndirizzoSpedizione");
            ViewBag.FKPizza = new SelectList(db.Pizze, "IdPizza", "Nome");
            return View();
        }

        // POST: Dettagli/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDettaglio,FKPizza,Quantita,FKOrdine")] Dettagli dettagli)
        {
            if (ModelState.IsValid)
            {
                db.Dettagli.Add(dettagli);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKOrdine = new SelectList(db.Ordini, "IdOrdine", "IndirizzoSpedizione", dettagli.FKOrdine);
            ViewBag.FKPizza = new SelectList(db.Pizze, "IdPizza", "Nome", dettagli.FKPizza);
            return View(dettagli);
        }

        // GET: Dettagli/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dettagli dettagli = db.Dettagli.Find(id);
            if (dettagli == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKOrdine = new SelectList(db.Ordini, "IdOrdine", "IndirizzoSpedizione", dettagli.FKOrdine);
            ViewBag.FKPizza = new SelectList(db.Pizze, "IdPizza", "Nome", dettagli.FKPizza);
            return View(dettagli);
        }

        // POST: Dettagli/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDettaglio,FKPizza,Quantita,FKOrdine")] Dettagli dettagli)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettagli).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKOrdine = new SelectList(db.Ordini, "IdOrdine", "IndirizzoSpedizione", dettagli.FKOrdine);
            ViewBag.FKPizza = new SelectList(db.Pizze, "IdPizza", "Nome", dettagli.FKPizza);
            return View(dettagli);
        }

        // GET: Dettagli/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dettagli dettagli = db.Dettagli.Find(id);
            if (dettagli == null)
            {
                return HttpNotFound();
            }
            return View(dettagli);
        }

        // POST: Dettagli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dettagli dettagli = db.Dettagli.Find(id);
            db.Dettagli.Remove(dettagli);
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
