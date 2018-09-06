using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GMed.Models;

namespace GMed.Controllers
{
    public class RendezVousController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RendezVous
        public ActionResult Index()
        {
            var rendezVous = db.RendezVous.Include(r => r.ActeMedical);
            return View(rendezVous.ToList());
        }

        // GET: RendezVous/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RendezVous rendezVous = db.RendezVous.Find(id);
            if (rendezVous == null)
            {
                return HttpNotFound();
            }
            return View(rendezVous);
        }

        // GET: RendezVous/Create
        public ActionResult Create()
        {
            ViewBag.RendezVousId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement");
            return View();
        }

        // POST: RendezVous/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RendezVousId,ActeMedicalId,DateRendezVous,AdresseRendezVous")] RendezVous rendezVous)
        {
            if (ModelState.IsValid)
            {
                db.RendezVous.Add(rendezVous);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RendezVousId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", rendezVous.RendezVousId);
            return View(rendezVous);
        }

        // GET: RendezVous/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RendezVous rendezVous = db.RendezVous.Find(id);
            if (rendezVous == null)
            {
                return HttpNotFound();
            }
            ViewBag.RendezVousId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", rendezVous.RendezVousId);
            return View(rendezVous);
        }

        // POST: RendezVous/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RendezVousId,ActeMedicalId,DateRendezVous,AdresseRendezVous")] RendezVous rendezVous)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rendezVous).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RendezVousId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", rendezVous.RendezVousId);
            return View(rendezVous);
        }

        // GET: RendezVous/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RendezVous rendezVous = db.RendezVous.Find(id);
            if (rendezVous == null)
            {
                return HttpNotFound();
            }
            return View(rendezVous);
        }

        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RendezVous rendezVous = db.RendezVous.Find(id);
            db.RendezVous.Remove(rendezVous);
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
