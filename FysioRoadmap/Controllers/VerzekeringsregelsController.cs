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
    public class VerzekeringsregelsController : Controller
    {
        private FRMModelContainer db = new FRMModelContainer();

        // GET: Verzekeringsregels
        public ActionResult Index()
        {
            var verzekeringsregelSet = db.VerzekeringsregelSet.Include(v => v.VerzekeringSet);
            return View(verzekeringsregelSet.ToList());
        }

        // GET: Verzekeringsregels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekeringsregel verzekeringsregel = db.VerzekeringsregelSet.Find(id);
            if (verzekeringsregel == null)
            {
                return HttpNotFound();
            }
            return View(verzekeringsregel);
        }

        // GET: Verzekeringsregels/Create
        public ActionResult Create()
        {
            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering");
            return View();
        }

        // POST: Verzekeringsregels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VerzekeringId,Minleeftijd,Maxleeftijd,BedragBehandeling,Jaartotaal")] Verzekeringsregel verzekeringsregel)
        {
            if (ModelState.IsValid)
            {
                db.VerzekeringsregelSet.Add(verzekeringsregel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering", verzekeringsregel.VerzekeringId);
            return View(verzekeringsregel);
        }

        // GET: Verzekeringsregels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekeringsregel verzekeringsregel = db.VerzekeringsregelSet.Find(id);
            if (verzekeringsregel == null)
            {
                return HttpNotFound();
            }
            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering", verzekeringsregel.VerzekeringId);
            return View(verzekeringsregel);
        }

        // POST: Verzekeringsregels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VerzekeringId,Minleeftijd,Maxleeftijd,BedragBehandeling,Jaartotaal")] Verzekeringsregel verzekeringsregel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verzekeringsregel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering", verzekeringsregel.VerzekeringId);
            return View(verzekeringsregel);
        }

        // GET: Verzekeringsregels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekeringsregel verzekeringsregel = db.VerzekeringsregelSet.Find(id);
            if (verzekeringsregel == null)
            {
                return HttpNotFound();
            }
            return View(verzekeringsregel);
        }

        // POST: Verzekeringsregels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verzekeringsregel verzekeringsregel = db.VerzekeringsregelSet.Find(id);
            db.VerzekeringsregelSet.Remove(verzekeringsregel);
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
