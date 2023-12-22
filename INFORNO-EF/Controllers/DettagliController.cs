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
        public ActionResult Index()     
        {
            //I need to get only the logged user details
            var userName = User.Identity.Name;
            var userId = db.Utenti.Where(m => m.Username == userName).FirstOrDefault().IdUtente;

            var order = db.Ordini.Where(m => m.FKUtente == userId).FirstOrDefault();
           
            if(order == null)
            { 
                ViewBag.ErrorMessage = "Il carrello è vuoto :(";
                return View();           
            }
            else
            {
                 var detailsList = order.Dettagli.ToList();
                 return View(detailsList);
            }
        }

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

        public ActionResult Create(int id)
        {
            //The pizza's id arrives  from Details view (Home controller). It's the pizza that costumer chose from the Home page
            var fkpizza = db.Pizze.Find(id).IdPizza;
            //I send this id to the HttpPost Create, in order to generate a Dettaglio with this FKPizza
            TempData["fkpizza"] = fkpizza;
            //I send it to the view so that I can use it for a better UX
            var nome = db.Pizze.Find(id).Nome;
            ViewBag.Nome = nome;

            ViewBag.FKOrdine = new SelectList(db.Ordini, "IdOrdine", "IndirizzoSpedizione");
            ViewBag.FKPizza = new SelectList(db.Pizze, "IdPizza", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Quantita")] Dettagli dettagli, int Quantita)
        {
             var fKpizza = TempData["fkpizza"];

            //Find user id
            var name = User.Identity.Name.ToString();
            var userId = db.Utenti.Where(m => m.Username == name).FirstOrDefault().IdUtente;

            //Check if the logged user already has an open order
            var userOrder = db.Ordini.Where(m => m.FKUtente == userId).FirstOrDefault();

            if (userOrder == null)
            {
                //If logged user has no orders, create a new one, having first saving the details data in a tempdata, in order to keep and use them later
                TempData["fkPizza"] = fKpizza;
                TempData["quantity"] = Quantita;

                return RedirectToAction("Create", "Ordini");
            }
            else
            {
                //If logged user already has an order, add a detail to it
                dettagli.FKOrdine = userOrder.IdOrdine;
                var pizza = Convert.ToInt32(fKpizza);
                dettagli.FKPizza = pizza;

                if (ModelState.IsValid)
                {
                    db.Dettagli.Add(dettagli);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.FKOrdine = new SelectList(db.Ordini, "IdOrdine", "IndirizzoSpedizione", dettagli.FKOrdine);
            ViewBag.FKPizza = new SelectList(db.Pizze, "IdPizza", "Nome", dettagli.FKPizza);
            return View(dettagli);
        }

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
