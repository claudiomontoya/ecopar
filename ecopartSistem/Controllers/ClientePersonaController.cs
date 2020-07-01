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
    public class ClientePersonaController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: ClientePersona
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
               return RedirectToAction("Index", "Cliente"); 
            }
            var cliente_Persona = db.Cliente_Persona.Include(c => c.clientes).Include(c => c.Personas).Include(c => c.tipo_persona).Where(x=>x.id_cliente==id).ToList();
            ViewBag.cliente = db.clientes.Where(x => x.id == id).FirstOrDefault();
            return View(cliente_Persona.ToList());
        }

        // GET: ClientePersona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente_Persona cliente_Persona = db.Cliente_Persona.Find(id);
            if (cliente_Persona == null)
            {
                return HttpNotFound();
            }
            return View(cliente_Persona);
        }

        // GET: ClientePersona/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Cliente");
            }
            ViewBag.cliente = db.clientes.Where(x => x.id == id).FirstOrDefault();
            ViewBag.id_persona = new SelectList(db.Personas, "id", "rut");
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre");
            return View();
        }

        // POST: ClientePersona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_cliente,id_persona,id_tipo")] Cliente_Persona cliente_Persona)
        {
            if (ModelState.IsValid)
            {
                db.Cliente_Persona.Add(cliente_Persona);
                db.SaveChanges();
                return RedirectToAction("Index",new { id = cliente_Persona.id_cliente });
            }

            ViewBag.cliente = db.clientes.Where(x => x.id == cliente_Persona.id_cliente).FirstOrDefault();
            ViewBag.id_persona = new SelectList(db.Personas, "id", "rut", cliente_Persona.id_persona);
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre", cliente_Persona.id_tipo);
            return View(cliente_Persona);
        }

        // GET: ClientePersona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente_Persona cliente_Persona = db.Cliente_Persona.Find(id);
            if (cliente_Persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", cliente_Persona.id_cliente);
            ViewBag.id_persona = new SelectList(db.Personas, "id", "rut", cliente_Persona.id_persona);
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre", cliente_Persona.id_tipo);
            return View(cliente_Persona);
        }

        // POST: ClientePersona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_cliente,id_persona,id_tipo")] Cliente_Persona cliente_Persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente_Persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", cliente_Persona.id_cliente);
            ViewBag.id_persona = new SelectList(db.Personas, "id", "rut", cliente_Persona.id_persona);
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre", cliente_Persona.id_tipo);
            return View(cliente_Persona);
        }

        // GET: ClientePersona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente_Persona cliente_Persona = db.Cliente_Persona.Find(id);
            if (cliente_Persona == null)
            {
                return HttpNotFound();
            }
            return View(cliente_Persona);
        }

        // POST: ClientePersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente_Persona cliente_Persona = db.Cliente_Persona.Find(id);
            db.Cliente_Persona.Remove(cliente_Persona);
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
