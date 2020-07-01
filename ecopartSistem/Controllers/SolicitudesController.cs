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
    public class SolicitudesController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Solicitudes
        public ActionResult Index(string estado="")
        {
            List<Solicitud> solicitud = new List<Solicitud>();
            if(estado=="pendiente")
            solicitud = db.Solicitud.Include(s => s.bodega).Include(s => s.clientes).Include(s => s.Productos).Include(s => s.sucursal).Include(s => s.usuarios).Where(x=>x.estado=="pendiente" || x.estado == "Emitida Completa" || x.estado == "Emitida Incompleta").ToList();
            else
            solicitud = db.Solicitud.Include(s => s.bodega).Include(s => s.clientes).Include(s => s.Productos).Include(s => s.sucursal).Include(s => s.usuarios).Where(x => x.estado == "recepcionada" || x.estado == "transferida" || x.estado == "finalizada" || x.estado == "cancelada").OrderByDescending(x=>x.id).ToList();
            return View(solicitud);
        }

        // GET: Solicitudes/Details/5
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
            ViewBag.emision = db.emision.Where(x => x.id_solicitud == id).ToList();
            return View(solicitud);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            ViewBag.id_bodega = new SelectList(db.bodega, "id", "descripcion");
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut");
            ViewBag.id_producto = new SelectList(db.Productos, "id", "producto");
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre");
            ViewBag.usuario = new SelectList(db.usuarios, "usuario", "nombre");
            return View();
        }

        // POST: Solicitudes/Create
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

            ViewBag.id_bodega = new SelectList(db.bodega, "id", "descripcion", solicitud.id_bodega);
            ViewBag.id_cliente = new SelectList(db.clientes, "id", "rut", solicitud.id_cliente);
            ViewBag.id_producto = new SelectList(db.Productos, "id", "producto", solicitud.id_producto);
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre", solicitud.id_sucursal);
            ViewBag.usuario = new SelectList(db.usuarios, "usuario", "nombre", solicitud.usuario);
            return View(solicitud);
        }

   
        public ActionResult Estado(int? id, string estado)
        {
            if (id == null || estado=="")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            solicitud.estado = estado;
            db.Entry(solicitud).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

       

        // GET: Solicitudes/Delete/5
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

        // POST: Solicitudes/Delete/5
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
