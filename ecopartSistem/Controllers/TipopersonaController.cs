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
    public class TipopersonaController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Tipopersona
        public ActionResult Index()
        {
            return View(db.tipo_persona.ToList());
        }

        // GET: Tipopersona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_persona tipo_persona = db.tipo_persona.Find(id);
            if (tipo_persona == null)
            {
                return HttpNotFound();
            }
            return View(tipo_persona);
        }

        // GET: Tipopersona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipopersona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] tipo_persona tipo_persona)
        {
            if (ModelState.IsValid)
            {
                db.tipo_persona.Add(tipo_persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_persona);
        }

        // GET: Tipopersona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_persona tipo_persona = db.tipo_persona.Find(id);
            if (tipo_persona == null)
            {
                return HttpNotFound();
            }
            return View(tipo_persona);
        }

        // POST: Tipopersona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] tipo_persona tipo_persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_persona);
        }

        // GET: Tipopersona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_persona tipo_persona = db.tipo_persona.Find(id);
            if (tipo_persona == null)
            {
                return HttpNotFound();
            }
            return View(tipo_persona);
        }

        // POST: Tipopersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tipo_persona tipo_persona = db.tipo_persona.Find(id);
            db.tipo_persona.Remove(tipo_persona);
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
