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
    public class OperationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Operations
        public ActionResult Index()
        {
            var acteMedicals = db.ActeMedicals.OfType<Operation>().Include(o => o.DMDPatient).Include(o => o.Facture).Include(o => o.RendezVous);
            return View(acteMedicals.ToList());
        }

        // GET: Operations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.ActeMedicals.OfType<Operation>().SingleOrDefault(s => s.OperationId == id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }

        // GET: Operations/Create
        public ActionResult Create()
        {
            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient");
            ViewBag.ActeMedicalId = new SelectList(db.Factures, "FactureId", "FactureId");
            ViewBag.ActeMedicalId = new SelectList(db.RendezVous, "RendezVousId", "DateRendezVous");
            return View();
        }

        // POST: Operations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActeMedicalId,DMDPatientId,FactureId,RendezVousId,Traitement,OperationId,AdresseOperation,IntituleOperation")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.ActeMedicals.Add(operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient", operation.DMDPatientId);
            ViewBag.ActeMedicalId = new SelectList(db.Factures, "FactureId", "FactureId", operation.ActeMedicalId);
            ViewBag.ActeMedicalId = new SelectList(db.RendezVous, "RendezVousId", "DateRendezVous", operation.ActeMedicalId);
            return View(operation);
        }

        // GET: Operations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.ActeMedicals.OfType<Operation>().SingleOrDefault(s => s.OperationId == id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient", operation.DMDPatientId);
            ViewBag.ActeMedicalId = new SelectList(db.Factures, "FactureId", "FactureId", operation.ActeMedicalId);
            ViewBag.ActeMedicalId = new SelectList(db.RendezVous, "RendezVousId", "DateRendezVous", operation.ActeMedicalId);
            return View(operation);
        }

        // POST: Operations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActeMedicalId,DMDPatientId,FactureId,RendezVousId,Traitement,OperationId,AdresseOperation,IntituleOperation")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient", operation.DMDPatientId);
            ViewBag.ActeMedicalId = new SelectList(db.Factures, "FactureId", "FactureId", operation.ActeMedicalId);
            ViewBag.ActeMedicalId = new SelectList(db.RendezVous, "RendezVousId", "DateRendezVous", operation.ActeMedicalId);
            return View(operation);
        }

        // GET: Operations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.ActeMedicals.OfType<Operation>().SingleOrDefault(s => s.OperationId == id);

            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operation operation = db.ActeMedicals.OfType<Operation>().SingleOrDefault(s => s.OperationId == id);

            db.ActeMedicals.Remove(operation);
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
