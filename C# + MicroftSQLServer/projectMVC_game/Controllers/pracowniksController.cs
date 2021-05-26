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
    public class pracowniksController : Controller
    {
        private project_gameEntities db = new project_gameEntities();

        // GET: pracowniks
        public ActionResult Index()
        {
            var pracowniks = db.pracowniks.Include(p => p.adres);
            return View(pracowniks.ToList());
        }

        // GET: pracowniks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pracownik pracownik = db.pracowniks.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            return View(pracownik);
        }

        // GET: pracowniks/Create
        public ActionResult Create()
        {
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "panstwo");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pracownik,imie,nazwisko,pesel,email,id_adres")] pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                db.pracowniks.Add(pracownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "panstwo", pracownik.id_adres);
            return View(pracownik);
        }

        // GET: pracowniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pracownik pracownik = db.pracowniks.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "panstwo", pracownik.id_adres);
            return View(pracownik);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pracownik,imie,nazwisko,pesel,email,id_adres")] pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "panstwo", pracownik.id_adres);
            return View(pracownik);
        }

        // GET: pracowniks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pracownik pracownik = db.pracowniks.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            return View(pracownik);
        }

        // POST: pracowniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pracownik pracownik = db.pracowniks.Find(id);
            db.pracowniks.Remove(pracownik);
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
