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
    public class BodegasController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Bodegas
        public ActionResult Index()
        {
            var bodega = db.bodega.Include(b => b.clientes).Include(b => b.comunas);
            return View(bodega.ToList());
        }

        // GET: Bodegas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.encargado = db.Personas.Where(x => x.id == bodega.id_encargado).FirstOrDefault();
            return View(bodega);
        }

        // GET: Bodegas/Create
        public ActionResult Create()
        {
            ViewBag.id_encargado = new SelectList(db.Cliente_Persona, "id", "id");
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut");
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna");
            return View();
        }

        // POST: Bodegas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_cliente,descripcion,id_comuna,id_encargado,direccion,numero,telefono")] bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.bodega.Add(bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_encargado = new SelectList(db.Cliente_Persona, "id", "id", bodega.id_encargado);
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", bodega.id_cliente);
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", bodega.id_comuna);
            return View(bodega);
        }

        // GET: Bodegas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_encargado = new SelectList(db.Cliente_Persona, "id", "id", bodega.id_encargado);
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", bodega.id_cliente);
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", bodega.id_comuna);
            return View(bodega);
        }

        // POST: Bodegas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_cliente,descripcion,id_comuna,id_encargado,direccion,numero,telefono")] bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_encargado = new SelectList(db.Cliente_Persona, "id", "id", bodega.id_encargado);
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", bodega.id_cliente);
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", bodega.id_comuna);
            return View(bodega);
        }

        // GET: Bodegas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: Bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bodega bodega = db.bodega.Find(id);
            db.bodega.Remove(bodega);
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
