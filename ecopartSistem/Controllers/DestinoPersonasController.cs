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
    public class DestinoPersonasController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: DestinoPersonas
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Destinos");
            }
            var destino_persona = db.destino_persona.Include(d => d.destinos).Include(d => d.Personas).Include(d => d.tipo_persona).Where(x=>x.id_destino==id).ToList();
            ViewBag.destino = db.destinos.Where(x => x.id == id).FirstOrDefault();
            return View(destino_persona.ToList());
        }

              // GET: DestinoPersonas/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Destinos");
            }
            ViewBag.destino = db.destinos.Where(x => x.id == id).FirstOrDefault();
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre");
            return View();
        }

        // POST: DestinoPersonas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_destino,id_persona,id_tipo")] destino_persona destino_persona)
        {
            if(destino_persona.id_persona==0)
            {
                ModelState.AddModelError(string.Empty, "Dede agregar un Contacto!!");
            }

            if (ModelState.IsValid)
            {
                db.destino_persona.Add(destino_persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.destino = db.destinos.Where(x => x.id == destino_persona.id_destino).FirstOrDefault();
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre", destino_persona.id_tipo);
            return View(destino_persona);
        }

        // GET: DestinoPersonas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destino_persona destino_persona = db.destino_persona.Find(id);
            if (destino_persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", destino_persona.id_destino);
            ViewBag.id_persona = new SelectList(db.Personas, "id", "rut", destino_persona.id_persona);
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre", destino_persona.id_tipo);
            return View(destino_persona);
        }

        // POST: DestinoPersonas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_destino,id_persona,id_tipo")] destino_persona destino_persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destino_persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_destino = new SelectList(db.destinos, "id", "nombre", destino_persona.id_destino);
            ViewBag.id_persona = new SelectList(db.Personas, "id", "rut", destino_persona.id_persona);
            ViewBag.id_tipo = new SelectList(db.tipo_persona, "id", "nombre", destino_persona.id_tipo);
            return View(destino_persona);
        }

        // GET: DestinoPersonas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destino_persona destino_persona = db.destino_persona.Find(id);
            if (destino_persona == null)
            {
                return HttpNotFound();
            }
            return View(destino_persona);
        }

        // POST: DestinoPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            destino_persona destino_persona = db.destino_persona.Find(id);
            db.destino_persona.Remove(destino_persona);
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
