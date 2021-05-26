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
    public class wypozyczeniesController : Controller
    {
        private project_gameEntities db = new project_gameEntities();

        // GET: wypozyczenies
        public ActionResult Index()
        {
            var wypozyczenies = db.wypozyczenies.Include(w => w.gra).Include(w => w.klient).Include(w => w.pracownik);
            return View(wypozyczenies.ToList());
        }

        // GET: wypozyczenies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wypozyczenie wypozyczenie = db.wypozyczenies.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // GET: wypozyczenies/Create
        public ActionResult Create()
        {
            ViewBag.id_gra = new SelectList(db.gras, "id_gra", "nazwa");
            ViewBag.id_klient = new SelectList(db.klients, "id_klient", "imie");
            ViewBag.id_pracownik = new SelectList(db.pracowniks, "id_pracownik", "imie");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_wypozyczenie,data_wypozyczenia,id_gra,id_pracownik,id_klient")] wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                db.wypozyczenies.Add(wypozyczenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_gra = new SelectList(db.gras, "id_gra", "nazwa", wypozyczenie.id_gra);
            ViewBag.id_klient = new SelectList(db.klients, "id_klient", "imie", wypozyczenie.id_klient);
            ViewBag.id_pracownik = new SelectList(db.pracowniks, "id_pracownik", "imie", wypozyczenie.id_pracownik);
            return View(wypozyczenie);
        }

        // GET: wypozyczenies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wypozyczenie wypozyczenie = db.wypozyczenies.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_gra = new SelectList(db.gras, "id_gra", "nazwa", wypozyczenie.id_gra);
            ViewBag.id_klient = new SelectList(db.klients, "id_klient", "imie", wypozyczenie.id_klient);
            ViewBag.id_pracownik = new SelectList(db.pracowniks, "id_pracownik", "imie", wypozyczenie.id_pracownik);
            return View(wypozyczenie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_wypozyczenie,data_wypozyczenia,id_gra,id_pracownik,id_klient")] wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_gra = new SelectList(db.gras, "id_gra", "nazwa", wypozyczenie.id_gra);
            ViewBag.id_klient = new SelectList(db.klients, "id_klient", "imie", wypozyczenie.id_klient);
            ViewBag.id_pracownik = new SelectList(db.pracowniks, "id_pracownik", "imie", wypozyczenie.id_pracownik);
            return View(wypozyczenie);
        }

        // GET: wypozyczenies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wypozyczenie wypozyczenie = db.wypozyczenies.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // POST: wypozyczenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wypozyczenie wypozyczenie = db.wypozyczenies.Find(id);
            db.wypozyczenies.Remove(wypozyczenie);
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
