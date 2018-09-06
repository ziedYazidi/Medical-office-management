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
    public class DocumentComplementairesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DocumentComplementaires
        public ActionResult Index()
        {
            var documentComplementaires = db.DocumentComplementaires.Include(d => d.ActeMedical);
            return View(documentComplementaires.ToList());
        }

        // GET: DocumentComplementaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentComplementaire documentComplementaire = db.DocumentComplementaires.Find(id);
            if (documentComplementaire == null)
            {
                return HttpNotFound();
            }
            return View(documentComplementaire);
        }

        // GET: DocumentComplementaires/Create
        public ActionResult Create()
        {
            ViewBag.ActeMedicalId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement");
            return View();
        }

        // POST: DocumentComplementaires/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentComplementaireId,ActeMedicalId,TypeDocumnet,TitreDocument,contenuDocument,Avis")] DocumentComplementaire documentComplementaire)
        {
            if (ModelState.IsValid)
            {
                db.DocumentComplementaires.Add(documentComplementaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActeMedicalId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", documentComplementaire.ActeMedicalId);
            return View(documentComplementaire);
        }

        // GET: DocumentComplementaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentComplementaire documentComplementaire = db.DocumentComplementaires.Find(id);
            if (documentComplementaire == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActeMedicalId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", documentComplementaire.ActeMedicalId);
            return View(documentComplementaire);
        }

        // POST: DocumentComplementaires/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentComplementaireId,ActeMedicalId,TypeDocumnet,TitreDocument,contenuDocument,Avis")] DocumentComplementaire documentComplementaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentComplementaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActeMedicalId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", documentComplementaire.ActeMedicalId);
            return View(documentComplementaire);
        }

        // GET: DocumentComplementaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentComplementaire documentComplementaire = db.DocumentComplementaires.Find(id);
            if (documentComplementaire == null)
            {
                return HttpNotFound();
            }
            return View(documentComplementaire);
        }

        // POST: DocumentComplementaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentComplementaire documentComplementaire = db.DocumentComplementaires.Find(id);
            db.DocumentComplementaires.Remove(documentComplementaire);
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
