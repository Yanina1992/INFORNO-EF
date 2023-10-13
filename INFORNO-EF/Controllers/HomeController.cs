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
        
        public ActionResult Create(int id) 
        {

         return View();
        }

        [HttpPost]
        public ActionResult Create(Ordini ordine, int id, DateTime Data, string IndirizzoSpedizione, string note, bool Concluso, string NomeCliente, int quantita) 
        {
            if (ModelState.IsValid)
            {
                Utenti utente = db.Utenti.Where(u => u.Username == NomeCliente).FirstOrDefault();
                int fkUt= ordine.FKUtente = utente.IdUtente;

                ordine.Dettagli.Add(new Dettagli
                { FKPizza = id, Quantita = quantita, FKOrdine = ordine.IdOrdine});

                db.Ordini.Add(ordine);
              

                db.SaveChanges();
                Session["NomeCliente"] = NomeCliente;
                Session["ordine"] = ordine;
                
                //TempData["FKUtente"] = fkUt;

                //return RedirectToAction("Carrello", "Cliente");
                return RedirectToAction("Index", "Clienti");
            }
            return View();
        }
    
    }
}
