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
    public class VerzekerdenController : Controller
    {
        private FRMModelContainer db = new FRMModelContainer();

        // GET: Verzekerden
        public ActionResult Index()
        {
            var verzekerdeSet = db.VerzekerdeSet; //.Include(v => v.VerzekeringSet);
            return View(verzekerdeSet.ToList());
        }

        // GET: Verzekerden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekerde verzekerde = db.VerzekerdeSet.Find(id);
            if (verzekerde == null)
            {
                return HttpNotFound();
            }
            return View(verzekerde);
        }

        // GET: Verzekerden/Create
        public ActionResult Create()
        {
            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering");
            return View();
        }

        // POST: Verzekerden/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NaamVerzekerde,Geboortedatum,VerzekeringId")] Verzekerde verzekerde)
        {
            if (ModelState.IsValid)
            {
                db.VerzekerdeSet.Add(verzekerde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering", verzekerde.VerzekeringId);
            return View(verzekerde);
        }

        // GET: Verzekerden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekerde verzekerde = db.VerzekerdeSet.Find(id);
            if (verzekerde == null)
            {
                return HttpNotFound();
            }
            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering", verzekerde.VerzekeringId);
            return View(verzekerde);
        }

        // POST: Verzekerden/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NaamVerzekerde,Geboortedatum,VerzekeringId")] Verzekerde verzekerde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verzekerde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VerzekeringId = new SelectList(db.VerzekeringSet, "Id", "Naamverzekering", verzekerde.VerzekeringId);
            return View(verzekerde);
        }

        // GET: Verzekerden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekerde verzekerde = db.VerzekerdeSet.Find(id);
            if (verzekerde == null)
            {
                return HttpNotFound();
            }
            return View(verzekerde);
        }

        // POST: Verzekerden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verzekerde verzekerde = db.VerzekerdeSet.Find(id);
            db.VerzekerdeSet.Remove(verzekerde);
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
