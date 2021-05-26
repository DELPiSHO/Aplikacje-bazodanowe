using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projectMVC_game.Models;

namespace projectMVC_game.Controllers
{
    public class grasController : Controller
    {
        private project_gameEntities db = new project_gameEntities();

        // GET: gras
        public ActionResult Index()
        {
            return View(db.gras.ToList());
        }

        // GET: gras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gra gra = db.gras.Find(id);
            if (gra == null)
            {
                return HttpNotFound();
            }
            return View(gra);
        }

        // GET: gras/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_gra,nazwa,ograniczenie_wiekowe,gatunek,platforma,cena,release_year")] gra gra)
        {
            if (ModelState.IsValid)
            {
                db.gras.Add(gra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gra);
        }

        // GET: gras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gra gra = db.gras.Find(id);
            if (gra == null)
            {
                return HttpNotFound();
            }
            return View(gra);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_gra,nazwa,ograniczenie_wiekowe,gatunek,platforma,cena,release_year")] gra gra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gra);
        }

        // GET: gras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gra gra = db.gras.Find(id);
            if (gra == null)
            {
                return HttpNotFound();
            }
            return View(gra);
        }

        // POST: gras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gra gra = db.gras.Find(id);
            db.gras.Remove(gra);
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
