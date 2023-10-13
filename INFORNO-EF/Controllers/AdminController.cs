using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}