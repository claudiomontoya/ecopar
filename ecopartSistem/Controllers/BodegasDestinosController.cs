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
    public class BodegasDestinosController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: BodegasDestinos
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Destinos");
            }
            ViewBag.destino = db.destinos.Where(x => x.id == id).FirstOrDefault();
            var bodegasdestinos = db.bodegasdestinos.Include(b => b.comunas).Include(b => b.destinos);
            return View(bodegasdestinos.ToList());
        }

        // GET: BodegasDestinos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodegasdestinos bodegasdestinos = db.bodegasdestinos.Find(id);
            if (bodegasdestinos == null)
            {
                return HttpNotFound();
            }
            ViewBag.encargado = db.Personas.Where(x => x.id == bodegasdestinos.id_encargado).FirstOrDefault();
            return View(bodegasdestinos);
        }

        // GET: BodegasDestinos/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Destinos");
            }
            ViewBag.destino = db.destinos.Where(x => x.id == id).FirstOrDefault();
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna");
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre");
            return View();
        }

        // POST: BodegasDestinos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descripcion,id_destino,id_comuna,id_encargado,direccion,numero,telefono")] bodegasdestinos bodegasdestinos)
        {
            if (ModelState.IsValid)
            {
                db.bodegasdestinos.Add(bodegasdestinos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.destino = db.destinos.Where(x => x.id == bodegasdestinos.id_destino).FirstOrDefault();
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", bodegasdestinos.id_comuna);
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", bodegasdestinos.id_destino);
            return View(bodegasdestinos);
        }

        // GET: BodegasDestinos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodegasdestinos bodegasdestinos = db.bodegasdestinos.Find(id);
            if (bodegasdestinos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", bodegasdestinos.id_comuna);
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", bodegasdestinos.id_destino);
            return View(bodegasdestinos);
        }

        // POST: BodegasDestinos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descripcion,id_destino,id_comuna,id_encargado,direccion,numero,telefono")] bodegasdestinos bodegasdestinos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodegasdestinos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", bodegasdestinos.id_comuna);
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", bodegasdestinos.id_destino);
            return View(bodegasdestinos);
        }

        // GET: BodegasDestinos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodegasdestinos bodegasdestinos = db.bodegasdestinos.Find(id);
            if (bodegasdestinos == null)
            {
                return HttpNotFound();
            }
            return View(bodegasdestinos);
        }

        // POST: BodegasDestinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bodegasdestinos bodegasdestinos = db.bodegasdestinos.Find(id);
            db.bodegasdestinos.Remove(bodegasdestinos);
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
