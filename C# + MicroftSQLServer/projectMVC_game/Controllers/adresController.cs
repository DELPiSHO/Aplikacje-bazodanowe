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
    public class adresController : Controller
    {
        private project_gameEntities db = new project_gameEntities();

        // GET: adres
        public ActionResult Index()
        {
            return View(db.adres.ToList());
        }

        // GET: adres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adres adres = db.adres.Find(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return View(adres);
        }

        // GET: adres/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_adres,panstwo,miasto,ulica,nr_domu,nr_pokoju")] adres adres)
        {
            if (ModelState.IsValid)
            {
                db.adres.Add(adres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adres);
        }

        // GET: adres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adres adres = db.adres.Find(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return View(adres);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_adres,panstwo,miasto,ulica,nr_domu,nr_pokoju")] adres adres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adres);
        }

        // GET: adres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adres adres = db.adres.Find(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return View(adres);
        }

        // POST: adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adres adres = db.adres.Find(id);
            db.adres.Remove(adres);
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
