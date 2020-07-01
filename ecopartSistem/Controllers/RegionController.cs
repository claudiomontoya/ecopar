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
    public class RegionController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Region
        public ActionResult Index()
        {
            var regiones = db.regiones.Include(r => r.paises);
            return View(regiones.ToList());
        }

        // GET: Region/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            regiones regiones = db.regiones.Find(id);
            if (regiones == null)
            {
                return HttpNotFound();
            }
            return View(regiones);
        }

        // GET: Region/Create
        public ActionResult Create()
        {
            ViewBag.id_pais = new SelectList(db.paises, "id", "pais");
            return View();
        }

        // POST: Region/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,region,id_pais")] regiones regiones)
        {
            if (ModelState.IsValid)
            {
                db.regiones.Add(regiones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pais = new SelectList(db.paises, "id", "pais", regiones.id_pais);
            return View(regiones);
        }

        // GET: Region/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            regiones regiones = db.regiones.Find(id);
            if (regiones == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pais = new SelectList(db.paises, "id", "pais", regiones.id_pais);
            return View(regiones);
        }

        // POST: Region/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,region,id_pais")] regiones regiones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regiones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pais = new SelectList(db.paises, "id", "pais", regiones.id_pais);
            return View(regiones);
        }

        // GET: Region/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            regiones regiones = db.regiones.Find(id);
            if (regiones == null)
            {
                return HttpNotFound();
            }
            return View(regiones);
        }

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            regiones regiones = db.regiones.Find(id);
            db.regiones.Remove(regiones);
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
