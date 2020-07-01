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
    public class DestinosClienteController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: ClienteDestinoes
        public ActionResult Index()
        {
            var clienteDestino = db.ClienteDestino.Include(c => c.clientes).Include(c => c.destinos).Include(c => c.Personas);
            return View(clienteDestino.ToList());
        }

        // GET: ClienteDestinoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDestino clienteDestino = db.ClienteDestino.Find(id);
            if (clienteDestino == null)
            {
                return HttpNotFound();
            }
            return View(clienteDestino);
        }

        // GET: ClienteDestinoes/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut");
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre");
            ViewBag.id_contacto = new SelectList(db.Personas, "id", "rut");
            return View();
        }

        // POST: ClienteDestinoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_cliente,id_destino,id_contacto,estado")] ClienteDestino clienteDestino)
        {
            if (ModelState.IsValid)
            {
                db.ClienteDestino.Add(clienteDestino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", clienteDestino.id_cliente);
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", clienteDestino.id_destino);
            ViewBag.id_contacto = new SelectList(db.Personas, "id", "rut", clienteDestino.id_contacto);
            return View(clienteDestino);
        }

        // GET: ClienteDestinoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDestino clienteDestino = db.ClienteDestino.Find(id);
            if (clienteDestino == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", clienteDestino.id_cliente);
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", clienteDestino.id_destino);
            ViewBag.id_contacto = new SelectList(db.Personas, "id", "rut", clienteDestino.id_contacto);
            return View(clienteDestino);
        }

        // POST: ClienteDestinoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_cliente,id_destino,id_contacto,estado")] ClienteDestino clienteDestino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteDestino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", clienteDestino.id_cliente);
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", clienteDestino.id_destino);
            ViewBag.id_contacto = new SelectList(db.Personas, "id", "rut", clienteDestino.id_contacto);
            return View(clienteDestino);
        }

        // GET: ClienteDestinoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDestino clienteDestino = db.ClienteDestino.Find(id);
            if (clienteDestino == null)
            {
                return HttpNotFound();
            }
            return View(clienteDestino);
        }

        // POST: ClienteDestinoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteDestino clienteDestino = db.ClienteDestino.Find(id);
            db.ClienteDestino.Remove(clienteDestino);
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
