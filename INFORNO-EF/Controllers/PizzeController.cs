using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INFORNO_EF.Models;

namespace INFORNO_EF.Controllers
{
    public class PizzeController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            return View(db.Pizze.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
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
                p.Nome = Nome;
                p.Prezzo = Prezzo;
                p.TempoConsegna = TempoConsegna;
                p.Ingredienti = Ingredienti;
                db.Pizze.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(p);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizze pizze = db.Pizze.Find(id);
            db.Pizze.Remove(pizze);
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
