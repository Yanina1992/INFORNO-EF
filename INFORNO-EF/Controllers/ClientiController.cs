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
        // GET: Clienti
        public ActionResult Index()
        {
            //var orUtente = db.Ordini.Where(m => m.FKUtente.ToString() == TempData["FKUtente"]);
            //orUtente.Include("Dettagli").ToList();

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

            //var trovaUtente = db.Utenti.Where(m => m.);
            //var trovaUtente = db.Utenti.Select(m => new { m.Username, m.IdUtente, m.Ordini}).Where(m => m.Username == nomeCliente);
            var trovaUtente = db.Utenti.Where(m => m.Username == nomeCliente).FirstOrDefault();
            var trovaOrdini= trovaUtente.Ordini.ToList();
            
            //var trovaId = db.Utenti.Select(m=> new {m.IdUtente}).Where
            //var trovaOrdine = db.Ordini.Select(m => new { m.FKUtente }).Where(m => m.FKUtente == trovaUtente.);
            
            return View(trovaOrdini);
        }
        public ActionResult Create() 
        {
            string FKPizza = TempData["FKPizza"] as string;
            string FKUtente = TempData["FKUtente"] as string;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Dettagli d)
        {
            return View();
        }
    }
}