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
    public class klientsController : Controller
    {
        private project_gameEntities db = new project_gameEntities();

        // GET: klients
        public ActionResult Index()
        {
            var klients = db.klients.Include(k => k.adres);
            return View(klients.ToList());
        }

        // GET: klients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klient klient = db.klients.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // GET: klients/Create
        public ActionResult Create()
        {
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "miasto");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_klient,imie,nazwisko,pesel,email,id_adres")] klient klient)
        {
            if (ModelState.IsValid)
            {
                db.klients.Add(klient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "miasto", klient.id_adres);
            return View(klient);
        }

        // GET: klients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klient klient = db.klients.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "miasto", klient.id_adres);
            return View(klient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_klient,imie,nazwisko,pesel,email,id_adres")] klient klient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "miasto", klient.id_adres);
            return View(klient);
        }

        // GET: klients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klient klient = db.klients.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // POST: klients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            klient klient = db.klients.Find(id);
            db.klients.Remove(klient);
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
