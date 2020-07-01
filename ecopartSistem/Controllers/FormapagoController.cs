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
    public class FormapagoController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Formapago
        public ActionResult Index()
        {
            return View(db.formapago.ToList());
        }

        // GET: Formapago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formapago formapago = db.formapago.Find(id);
            if (formapago == null)
            {
                return HttpNotFound();
            }
            return View(formapago);
        }

        // GET: Formapago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Formapago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] formapago formapago)
        {
            if (ModelState.IsValid)
            {
                db.formapago.Add(formapago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formapago);
        }

        // GET: Formapago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formapago formapago = db.formapago.Find(id);
            if (formapago == null)
            {
                return HttpNotFound();
            }
            return View(formapago);
        }

        // POST: Formapago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] formapago formapago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formapago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formapago);
        }

        // GET: Formapago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formapago formapago = db.formapago.Find(id);
            if (formapago == null)
            {
                return HttpNotFound();
            }
            return View(formapago);
        }

        // POST: Formapago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            formapago formapago = db.formapago.Find(id);
            db.formapago.Remove(formapago);
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
