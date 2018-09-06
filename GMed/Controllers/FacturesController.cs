using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GMed.Models;
using System.Web.Helpers;

namespace GMed.Controllers
{
    public class FacturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Factures
        public ActionResult Index()
        {
            var factures = db.Factures.Include(f => f.ActeMedical);
            return View(factures.ToList());
        }

        // GET: Factures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return HttpNotFound();
            }
            return View(facture);
        }

        // GET: Factures/Create
        public ActionResult Create()
        {
            ViewBag.FactureId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement");
            return View();
        }

        // POST: Factures/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FactureId,ActeMedicalId,EtatReglement,Montant")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                db.Factures.Add(facture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FactureId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", facture.FactureId);
            return View(facture);
        }

        // GET: Factures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactureId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", facture.FactureId);
            return View(facture);
        }
        public ActionResult MyChart()
        {
            SampleDataContext data = new SampleDataContext();
            decimal Janvier = 0;
            decimal Fevrier = 0;
            decimal Mars = 0;
            decimal Avril = 0;
            decimal Mai = 0;
            decimal Juin = 0;
            decimal Juillet = 0;
            decimal Aout = 0;
            decimal Septembre = 0;
            decimal Octobre = 0;
            decimal Novembre = 0;
            decimal Decemebre = 0;
            var donnée = data.CalendarEvents;
            foreach (var item in donnée)
            {
                switch (item.start_date.Month)
                {
                    case 1:
                        Janvier++;
                        break;

                    case 2:
                        Fevrier++;
                        break;

                    case 3:
                        Mars++;
                        break;

                    case 4:
                        Avril++;
                        break;

                    case 5:
                        Mai++;
                        break;

                    case 6:
                        Juin++;
                        break;

                    case 7:
                        Juillet++;
                        break;

                    case 8:
                        Aout++;
                        break;

                    case 9:
                        Septembre++;
                        break;

                    case 10:
                        Octobre++;
                        break;

                    case 11:
                        Novembre++;
                        break;

                    case 12:
                        Decemebre++;
                        break;
                }

            }
           
            new Chart(width: 750, height: 200)
                .AddSeries(
                    chartType: "column",
                    xValue: new[] { "Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Decembre" },
                    yValues: new[] { Janvier, Fevrier, Mars, Avril, Mai, Juin, Juillet, Aout, Septembre, Octobre, Novembre, Decemebre })
                .Write("png");
            return null;
        }

        public ActionResult BillChart()
        {
            SampleDataContext data = new SampleDataContext();
            decimal Janvier = 0;
            decimal Fevrier = 0;
            decimal Mars = 0;
            decimal Avril = 0;
            decimal Mai = 0;
            decimal Juin = 0;
            decimal Juillet = 0;
            decimal Aout = 0;
            decimal Septembre = 0;
            decimal Octobre = 0;
            decimal Novembre = 0;
            decimal Decemebre = 0;
            var donnée = data.CalendarEvents;
            foreach (var item in donnée)
            {
                switch (item.start_date.Month)
                {
                    case 1:
                        Janvier++;
                        break;

                    case 2:
                        Fevrier++;
                        break;

                    case 3:
                        Mars++;
                        break;

                    case 4:
                        Avril++;
                        break;

                    case 5:
                        Mai++;
                        break;

                    case 6:
                        Juin++;
                        break;

                    case 7:
                        Juillet++;
                        break;

                    case 8:
                        Aout++;
                        break;

                    case 9:
                        Septembre++;
                        break;

                    case 10:
                        Octobre++;
                        break;

                    case 11:
                        Novembre++;
                        break;

                    case 12:
                        Decemebre++;
                        break;
                }

            }

            string myTheme =
                @"<Chart BackColor=""Transparent"">
                                    <ChartAreas>
                                        <ChartArea Name=""Default"" BackColor=""Transparent""></ChartArea>
                                    </ChartAreas>
                                </Chart>";
            new Chart(width: 300, height: 350, theme: myTheme)
                .AddSeries(
                    chartType: "pie",
                    xValue: new[] { "Jan", "Fev", "Mars", "Avr", "Mai", "Juin", "Juil", "Aout", "Sept", "Oct", "Nov", "Dec" },
                    yValues: new[] { Janvier, Fevrier, Mars, Avril, Mai, Juin, Juillet, Aout, Septembre, Octobre, Novembre, Decemebre })
                .Write("png");
            return null;
        }
       

        // POST: Factures/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FactureId,ActeMedicalId,EtatReglement,Montant")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactureId = new SelectList(db.ActeMedicals, "ActeMedicalId", "Traitement", facture.FactureId);
            return View(facture);
        }

        // GET: Factures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return HttpNotFound();
            }
            return View(facture);
        }

        // POST: Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facture facture = db.Factures.Find(id);
            db.Factures.Remove(facture);
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
