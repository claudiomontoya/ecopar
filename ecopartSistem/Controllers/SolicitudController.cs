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
    public class SolicitudController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Solicitud
        public ActionResult Index()
        {
            var solicitud = db.Solicitud.Include(s => s.bodega).Include(s => s.clientes).Include(s => s.Productos).Include(s => s.sucursal).Include(s => s.usuarios).OrderByDescending(x=>x.id);
            return View(solicitud.ToList());
        }

        // GET: Solicitud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // GET: Solicitud/Create
        public ActionResult Create()
        {
            ViewBag.estado = "pendiente";
            ViewBag.fecha = DateTime.Now.ToShortDateString();
            ViewBag.cliente = 1;
            ViewBag.usuario = "prueba";
            ViewBag.id_producto = new SelectList(db.Productos, "id", "producto");
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre");
            ViewBag.id_bodega = db.bodega.Where(x => x.id_cliente == 1).ToList();

            return View();
        }

        // POST: Solicitud/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fecha,id_cliente,usuario,estado,descripcion,archivo,id_producto,cantidad,tipo_despacho,fecha_requerida,id_bodega,id_sucursal")] Solicitud solicitud)
        {
           
            if (ModelState.IsValid)
            {
                db.Solicitud.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.Productos, "id", "producto");
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre");
            ViewBag.id_bodega = new SelectList(db.bodega, "id", "descripcion");
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", solicitud.id_cliente);
            ViewBag.usuario = new SelectList(db.usuarios, "usuario", "nombre", solicitud.usuario);
            return View(solicitud);
        }

        // GET: Solicitud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", solicitud.id_cliente);
            ViewBag.usuario = new SelectList(db.usuarios, "usuario", "nombre", solicitud.usuario);
            return View(solicitud);
        }

        // POST: Solicitud/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha,id_cliente,usuario,estado,descripcion,archivo")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", solicitud.id_cliente);
            ViewBag.usuario = new SelectList(db.usuarios, "usuario", "nombre", solicitud.usuario);
            return View(solicitud);
        }

        // GET: Solicitud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: Solicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            db.Solicitud.Remove(solicitud);
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
