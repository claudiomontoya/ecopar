using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace ecopartSistem.Controllers
{
    public class ComunaController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Comuna
        public ActionResult Index()
        {
            var comunas = db.comunas.Include(c => c.regiones);
            return View(comunas.ToList());
        }

        // GET: Comuna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comunas comunas = db.comunas.Find(id);
            if (comunas == null)
            {
                return HttpNotFound();
            }
            return View(comunas);
        }

        // GET: Comuna/Create
        public ActionResult Create()
        {
            ViewBag.id_region = new SelectList(db.regiones, "id", "region");
            return View();
        }

        // POST: Comuna/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,comuna,id_region")] comunas comunas)
        {
            if (ModelState.IsValid)
            {
                db.comunas.Add(comunas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_region = new SelectList(db.regiones, "id", "region", comunas.id_region);
            return View(comunas);
        }

        // GET: Comuna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comunas comunas = db.comunas.Find(id);
            if (comunas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_region = new SelectList(db.regiones, "id", "region", comunas.id_region);
            return View(comunas);
        }

        // POST: Comuna/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,comuna,id_region")] comunas comunas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comunas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_region = new SelectList(db.regiones, "id", "region", comunas.id_region);
            return View(comunas);
        }

        // GET: Comuna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comunas comunas = db.comunas.Find(id);
            if (comunas == null)
            {
                return HttpNotFound();
            }
            return View(comunas);
        }

        // POST: Comuna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comunas comunas = db.comunas.Find(id);
            db.comunas.Remove(comunas);
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
