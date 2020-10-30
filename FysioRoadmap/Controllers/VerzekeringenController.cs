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
    public class VerzekeringenController : Controller
    {
        private FRMModelContainer db = new FRMModelContainer();

        // GET: Verzekeringen
        public ActionResult Index()
        {
            return View(db.VerzekeringSet.ToList());
        }

        // GET: Verzekeringen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekering verzekering = db.VerzekeringSet.Find(id);
            if (verzekering == null)
            {
                return HttpNotFound();
            }
            return View(verzekering);
        }

        // GET: Verzekeringen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verzekeringen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naamverzekering")] Verzekering verzekering)
        {
            if (ModelState.IsValid)
            {
                db.VerzekeringSet.Add(verzekering);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(verzekering);
        }

        // GET: Verzekeringen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekering verzekering = db.VerzekeringSet.Find(id);
            if (verzekering == null)
            {
                return HttpNotFound();
            }
            return View(verzekering);
        }

        // POST: Verzekeringen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naamverzekering")] Verzekering verzekering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verzekering).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(verzekering);
        }

        // GET: Verzekeringen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verzekering verzekering = db.VerzekeringSet.Find(id);
            if (verzekering == null)
            {
                return HttpNotFound();
            }
            return View(verzekering);
        }

        // POST: Verzekeringen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verzekering verzekering = db.VerzekeringSet.Find(id);
            db.VerzekeringSet.Remove(verzekering);
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
