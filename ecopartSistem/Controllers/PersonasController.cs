using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo;
using ecopartSistem.Models;

namespace ecopartSistem.Controllers
{
    public class PersonasController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Personas
        public ActionResult Index()
        {
            return View(db.Personas.ToList());
        }

        public JsonResult GetPersonas(string nombre)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Personas> PersonaListado = new List<Personas>();
            if (nombre != "")
                PersonaListado = db.Personas.Where(c => c.nombre.Contains(nombre)).Take(10).ToList();
            else
                PersonaListado = db.Personas.Take(10).ToList();

            return Json(PersonaListado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonasCliente(int id)
        {
            List<Cliente_Persona> clientepersona = db.Cliente_Persona.Include(c=>c.Personas).Include(c=>c.tipo_persona).Where(x => x.id_cliente == id).ToList();
            List<PersonaModel> persona = new List<PersonaModel>();
            foreach (var item in clientepersona)
            {
                PersonaModel per = new PersonaModel();
                per.rut = item.Personas.rut;
                per.nombre = item.Personas.rut;
                per.apellido = item.Personas.apellido;
                per.celular = item.Personas.celular;
                per.id = item.Personas.id;
                per.tipo = item.tipo_persona.nombre;
                persona.Add(per);
            }
           

            return Json(persona, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonasDestino(int id)
        {
            List<destino_persona> clientepersona = db.destino_persona.Include(c => c.Personas).Include(c => c.tipo_persona).Where(x => x.id_destino == id).ToList();
            List<PersonaModel> persona = new List<PersonaModel>();
            foreach (var item in clientepersona)
            {
                PersonaModel per = new PersonaModel();
                per.rut = item.Personas.rut;
                per.nombre = item.Personas.rut;
                per.apellido = item.Personas.apellido;
                per.celular = item.Personas.celular;
                per.id = item.Personas.id;
                per.tipo = item.tipo_persona.nombre;
                persona.Add(per);
            }


            return Json(persona, JsonRequestBehavior.AllowGet);
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,rut,nombre,apellido,telefono,celular,estado,mail")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(personas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,rut,nombre,apellido,telefono,celular,estado,mail")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personas);
        }

    
        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personas personas = db.Personas.Find(id);
            db.Personas.Remove(personas);
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
