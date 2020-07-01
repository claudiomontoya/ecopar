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
    public class EmisionController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Emision
        public ActionResult Index(string estado="pendientes")
        {
            List<emision> emision = new List<Modelo.emision>();

            if (estado == "pendientes")
            {
                var cliente = 1;
                var solicitudes = db.Solicitud.Where(x => x.id_cliente == cliente && x.estado == "recepcionada").ToList();
                List<int> solictudesId = new List<int>();
                foreach (var item in solicitudes)
                {
                    solictudesId.Add(item.id);
                }
                ViewBag.titulo = "Listado de Emisiones por Transferir";
                emision = db.emision.Where(x => solictudesId.Any(y => y == x.id_solicitud) && x.estado == "recepcionada").Include(e => e.Solicitud).ToList();
            }
            else
            {
                var cliente = 1;
                var solicitudes = db.Solicitud.Where(x => x.id_cliente == cliente).ToList();
                List<int> solictudesId = new List<int>();
                foreach (var item in solicitudes)
                {
                    solictudesId.Add(item.id);
                }
                ViewBag.titulo = "Listado de Emisiones Transferidas";
                emision = db.emision.Where(x => solictudesId.Any(y => y == x.id_solicitud) && x.estado != "recepcionada").Include(e => e.Solicitud).ToList();

            }
            return View(emision);
        }

        // GET: Emision/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emision emision = db.emision.Find(id);
            if (emision == null)
            {
                return HttpNotFound();
            }
            return View(emision);
        }

        // GET: Emision/Create
        public ActionResult Create(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index","Solicitudes");
            }
            ViewBag.solicitud = db.Solicitud.Include(e => e.Productos).Include(e => e.clientes).Where(x => x.id == id).FirstOrDefault();
            if (ViewBag.solicitud == null)
            {
                return RedirectToAction("Index", "Solicitudes");
            }

            return View();
        }

        // POST: Emision/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_solicitud,fecha,usuarioeco,cantidad,estado")] emision emision)
        {
            emision.fecha = DateTime.Now;
            emision.usuarioeco = "Admin";
            emision.estado = "iniciada";
            int cantidad = 0;
            var cantidades = db.emision.Where(x => x.id_solicitud == emision.id_solicitud).ToList();
            foreach (var item in cantidades)
            {
                cantidad += item.cantidad;
            }
            cantidad += emision.cantidad;




            var solicitud = db.Solicitud.Where(x => x.id == emision.id_solicitud).FirstOrDefault().cantidad;

            if (cantidad > solicitud || emision.cantidad<1)
            {
                ModelState.AddModelError("cantidad", "La Cantidad Supera a la Solicitada");
                ViewBag.solicitud = db.Solicitud.Include(e => e.Productos).Include(e => e.clientes).Where(x => x.id == emision.id_solicitud).FirstOrDefault();
                return View(emision);
            }

            else
            {
                db.emision.Add(emision);
                db.SaveChanges();
                var soli = db.Solicitud.Where(x => x.id == emision.id_solicitud).FirstOrDefault();
                if(cantidad==solicitud)
                soli.estado = "Emitida Completa";
                else
                soli.estado = "Emitida Incompleta";

                db.Entry(soli).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Solicitudes");
            }

           
        }

        // GET: Emision/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emision emision = db.emision.Find(id);
            if (emision == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_solicitud = new SelectList(db.Solicitud, "id", "usuario", emision.id_solicitud);
            return View(emision);
        }

        // POST: Emision/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_solicitud,fecha,usuarioeco,cantidad,estado")] emision emision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_solicitud = new SelectList(db.Solicitud, "id", "usuario", emision.id_solicitud);
            return View(emision);
        }

        // GET: Emision/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emision emision = db.emision.Find(id);
            if (emision == null)
            {
                return HttpNotFound();
            }
            return View(emision);
        }

        // POST: Emision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            emision emision = db.emision.Find(id);
            db.emision.Remove(emision);
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

        public ActionResult Estado(int? id, string estado)
        {
            if (id == null || estado == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emision emi = db.emision.Find(id);
            emi.estado = estado;
            db.Entry(emi).State = EntityState.Modified;
            db.SaveChanges();

            var cuenta = db.emision.Where(x=>x.id_solicitud==emi.id_solicitud && x.estado=="iniciada").Count();
            if (cuenta == 0)
            {
                Solicitud solicitud = db.Solicitud.Where(x => x.id == emi.id_solicitud).FirstOrDefault();
                solicitud.estado = "recepcionada";
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("../Solicitudes/Index?estado=pendiente");

        }
    }
}
