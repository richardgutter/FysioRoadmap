using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FysioRoadmap.Models;

namespace FysioRoadmap.Controllers
{
    public class DeclaratiesController : Controller
    {
        private FRMModelContainer db = new FRMModelContainer();

        // GET: Declaraties
        public ActionResult Index()
        {
            List<Declaratie> declaratieList = new List<Declaratie>();

            declaratieList = Berekening();
            return View(declaratieList);
        }

        // GET: Declaraties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Declaratie declaratie = db.DeclaratieSet.Find(id);
            if (declaratie == null)
            {
                return HttpNotFound();
            }
            return View(declaratie);
        }

        // GET: Declaraties/Create
        public ActionResult Create()
        {
            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde");
            return View();
        }

        // POST: Declaraties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatumBehandeling,DeclaratieBedrag,Totaal,VerzekerdeId")] Declaratie declaratie)
        {
            if (ModelState.IsValid)
            {
                db.DeclaratieSet.Add(declaratie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde", declaratie.VerzekerdeId);
            return View(declaratie);
        }

        // GET: Declaraties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Declaratie declaratie = db.DeclaratieSet.Find(id);
            if (declaratie == null)
            {
                return HttpNotFound();
            }
            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde", declaratie.VerzekerdeId);
            return View(declaratie);
        }

        // POST: Declaraties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatumBehandeling,DeclaratieBedrag,Totaal,VerzekerdeId")] Declaratie declaratie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(declaratie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde", declaratie.VerzekerdeId);
            return View(declaratie);
        }

        // GET: Declaraties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Declaratie declaratie = db.DeclaratieSet.Find(id);
            if (declaratie == null)
            {
                return HttpNotFound();
            }
            return View(declaratie);
        }

        // POST: Declaraties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Declaratie declaratie = db.DeclaratieSet.Find(id);
            db.DeclaratieSet.Remove(declaratie);
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

        protected List<Declaratie> Berekening()
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            int leeftijd;
            TimeSpan duration;
            List<Declaratie> declaratieList = new List<Declaratie>();
            decimal totgedeclareerd;
            decimal vorigtotgedeclareerd;

            var verzekerdeSet = db.VerzekerdeSet.Include(v => v.VerzekeringSet).Include(a => a.AfsprakenSet);
            List<Verzekerde> verzekerdenList = verzekerdeSet.ToList();

            foreach (Verzekerde verzekerde in verzekerdenList)
            {
                List<Verzekeringsregel> verzekeringsregelList = verzekerde.VerzekeringSet.VerzekeringsregelSet.ToList();
                List<Afspraak> afsprakenList = verzekerde.AfsprakenSet.ToList();
                totgedeclareerd = 0;
                foreach (Afspraak afspraak in afsprakenList)
                {
                    duration = afspraak.Datum - verzekerde.Geboortedatum;
                    leeftijd = (zeroTime + duration).Year - 1;
                    foreach (Verzekeringsregel regel in verzekeringsregelList)
                    {
                        if (leeftijd >= regel.Minleeftijd && leeftijd <= regel.Maxleeftijd)
                        {
                            Declaratie declaratie = new Declaratie();
                            declaratie.DatumBehandeling = afspraak.Datum;
                            vorigtotgedeclareerd = totgedeclareerd;
                            totgedeclareerd = totgedeclareerd + regel.BedragBehandeling;
                            if (totgedeclareerd > regel.Jaartotaal)
                            {
                                declaratie.DeclaratieBedrag = regel.Jaartotaal - vorigtotgedeclareerd;
                                declaratie.Totaal = regel.Jaartotaal;
                                totgedeclareerd = regel.Jaartotaal;
                            }
                            else
                            {
                                declaratie.DeclaratieBedrag = regel.BedragBehandeling;
                                declaratie.Totaal = totgedeclareerd;
                            }
                            declaratie.VerzekerdeId = verzekerde.Id;
                            declaratie.VerzekerdeSet = verzekerde;

                            declaratieList.Add(declaratie);
                        }
                    }
                }
            }
            return declaratieList;
        }
    }
}