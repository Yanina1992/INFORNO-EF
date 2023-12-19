using INFORNO_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFORNO_EF.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            var nomeCliente = Session["NomeCliente"] as string;
            var ordine = Session["ordine"] as string;

            if(nomeCliente != null)
            {
                ViewBag.nomeCliente= nomeCliente;
            }

            if(ordine != null)
            {
                ViewBag.ordine= ordine;
            }

            var trovaUtente = db.Utenti.Where(m => m.Username == nomeCliente).FirstOrDefault();
            var trovaOrdini= trovaUtente.Ordini.ToList();
            
            return View(trovaOrdini);
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
                int fkUt = ordine.FKUtente = utente.IdUtente;

                ordine.Dettagli.Add(new Dettagli
                { FKPizza = id, Quantita = quantita, FKOrdine = ordine.IdOrdine });

                db.Ordini.Add(ordine);

                db.SaveChanges();
                Session["NomeCliente"] = NomeCliente;
                Session["ordine"] = ordine;

                return RedirectToAction("Index", "Clienti");
            }
            return View();
        }

    }
}