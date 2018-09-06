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
    public class ConsultationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Consultations
        



        public ActionResult Index(int? id)
        {
            var acteMedicals = db.ActeMedicals.OfType<Consultation>().Include(c => c.DMDPatient).Include(c => c.Facture).Include(c => c.RendezVous);
            var result = from x in acteMedicals
                         where x.DMDPatientId == id
                         select x;

            return View(result.ToList());
        }

        // GET: Consultations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.ActeMedicals.OfType<Consultation>().SingleOrDefault(s => s.ConsultationId == id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // GET: Consultations/Create
        public ActionResult Create()
        {   
            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient");
            
            return View();
        }

        // POST: Consultations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActeMedicalId,DMDPatientId,Traitement,MotifConsultation,HistoireMaladie,ExamenClinique,DiagnostiqueFinal,traitementPropose")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.ActeMedicals.Add(consultation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient", consultation.DMDPatientId);
            
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             Consultation consultation = db.ActeMedicals.OfType<Consultation>().SingleOrDefault(s => s.ConsultationId == id);
          
            if (consultation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient", consultation.DMDPatientId);
           
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActeMedicalId,DMDPatientId,Traitement,MotifConsultation,HistoireMaladie,ExamenClinique,DiagnostiqueFinal,traitementPropose")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DMDPatientId = new SelectList(db.DMDPatients, "DMDPatientId", "NomPatient", consultation.DMDPatientId);
           
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.ActeMedicals.OfType<Consultation>().SingleOrDefault(s => s.ConsultationId == id);

            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultation consultation = db.ActeMedicals.OfType<Consultation>().SingleOrDefault(s => s.ConsultationId == id);

            db.ActeMedicals.Remove(consultation);
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
