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
    public class AfsprakenController : Controller
    {
        private FRMModelContainer db = new FRMModelContainer();

        // GET: Afspraken
        public ActionResult Index()
        {
            var afspraakSet = db.AfspraakSet.Include(a => a.VerzekerdeSet);
            return View(afspraakSet.ToList());
        }

        // GET: Afspraken/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afspraak afspraak = db.AfspraakSet.Find(id);
            if (afspraak == null)
            {
                return HttpNotFound();
            }
            return View(afspraak);
        }

        // GET: Afspraken/Create
        public ActionResult Create()
        {
            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde");
            return View();
        }

        // POST: Afspraken/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Datum,VerzekerdeId")] Afspraak afspraak)
        {
            if (ModelState.IsValid)
            {
                db.AfspraakSet.Add(afspraak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde", afspraak.VerzekerdeId);
            return View(afspraak);
        }

        // GET: Afspraken/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afspraak afspraak = db.AfspraakSet.Find(id);
            if (afspraak == null)
            {
                return HttpNotFound();
            }
            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde", afspraak.VerzekerdeId);
            return View(afspraak);
        }

        // POST: Afspraken/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Datum,VerzekerdeId")] Afspraak afspraak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afspraak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VerzekerdeId = new SelectList(db.VerzekerdeSet, "Id", "NaamVerzekerde", afspraak.VerzekerdeId);
            return View(afspraak);
        }

        // GET: Afspraken/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afspraak afspraak = db.AfspraakSet.Find(id);
            if (afspraak == null)
            {
                return HttpNotFound();
            }
            return View(afspraak);
        }

        // POST: Afspraken/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Afspraak afspraak = db.AfspraakSet.Find(id);
            db.AfspraakSet.Remove(afspraak);
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
