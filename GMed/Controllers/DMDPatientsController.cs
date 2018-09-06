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
    public class DMDPatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DMDPatients
        public ActionResult Index()
        {
            return View(db.DMDPatients.ToList());
        }

        // GET: DMDPatients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DMDPatient dMDPatient = db.DMDPatients.Include(x => x.ActeMedical).FirstOrDefault(p => p.DMDPatientId == id);
            
           
            if (dMDPatient == null)
            {
                return HttpNotFound();
            }
            return View(dMDPatient);
        }

        // GET: DMDPatients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DMDPatients/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DMDPatientId,NomPatient,PrenomPatient,TelPatient,MailPatient,AdressePatient,DateDeNaissance,AntecedentsFamiliaux,AntecedentsPersonnel")] DMDPatient dMDPatient)
        {
            if (ModelState.IsValid)
            {
                db.DMDPatients.Add(dMDPatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dMDPatient);
        }

        // GET: DMDPatients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMDPatient dMDPatient = db.DMDPatients.Find(id);
            if (dMDPatient == null)
            {
                return HttpNotFound();
            }
            return View(dMDPatient);
        }

        // POST: DMDPatients/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DMDPatientId,NomPatient,PrenomPatient,TelPatient,MailPatient,AdressePatient,DateDeNaissance,AntecedentsFamiliaux,AntecedentsPersonnel")] DMDPatient dMDPatient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dMDPatient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dMDPatient);
        }

        // GET: DMDPatients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMDPatient dMDPatient = db.DMDPatients.Find(id);
            if (dMDPatient == null)
            {
                return HttpNotFound();
            }
            return View(dMDPatient);
        }

        // POST: DMDPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DMDPatient dMDPatient = db.DMDPatients.Find(id);
            db.DMDPatients.Remove(dMDPatient);
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
