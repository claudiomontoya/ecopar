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
    public class TranferenciaBodegaController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: TranferenciaBodega
        public ActionResult Index()
        {
            var tranferencia_bodega = db.tranferencia_bodega.Include(t => t.emision).Include(t => t.sucursal);
            return View(tranferencia_bodega.ToList());
        }

        // GET: TranferenciaBodega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranferencia_bodega tranferencia_bodega = db.tranferencia_bodega.Find(id);
            if (tranferencia_bodega == null)
            {
                return HttpNotFound();
            }
            return View(tranferencia_bodega);
        }

        // GET: TranferenciaBodega/Create
        public ActionResult Create(int id)
        {
            ViewBag.id_emision = id;
            ViewBag.cantidad = db.emision.Where(x => x.id == id).FirstOrDefault().cantidad;
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre");
            return View();
        }

        // POST: TranferenciaBodega/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_emision,id_sucursal,fecha_creacion,usuario,archivo,fecha_retiro,cantidad,estado,fecha_envio,tipo")] tranferencia_bodega tranferencia_bodegax)
        {
            tranferencia_bodegax.usuario = "Admin";
            tranferencia_bodegax.fecha_creacion = DateTime.Now;
            tranferencia_bodegax.estado = "Iniciada";


            if (true)
            {
              
                db.tranferencia_bodega.Add(tranferencia_bodegax);
                db.SaveChanges();

                emision emi = db.emision.Where(x => x.id == tranferencia_bodegax.id_emision).FirstOrDefault();
                emi.estado = "Transferida a Ecopar";
                db.Entry(emi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_emision = tranferencia_bodegax.id_emision;
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre", tranferencia_bodegax.id_sucursal);
            return View(tranferencia_bodegax);
        }

        // GET: TranferenciaBodega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranferencia_bodega tranferencia_bodega = db.tranferencia_bodega.Find(id);
            if (tranferencia_bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_emision = new SelectList(db.emision, "id", "usuarioeco", tranferencia_bodega.id_emision);
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre", tranferencia_bodega.id_sucursal);
            return View(tranferencia_bodega);
        }

        // POST: TranferenciaBodega/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_emision,id_sucursal,fecha_creacion,usuario,archivo,fecha_retiro,cantidad,estado,fecha_envio,tipo")] tranferencia_bodega tranferencia_bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tranferencia_bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_emision = new SelectList(db.emision, "id", "usuarioeco", tranferencia_bodega.id_emision);
            ViewBag.id_sucursal = new SelectList(db.sucursal, "id", "nombre", tranferencia_bodega.id_sucursal);
            return View(tranferencia_bodega);
        }

        // GET: TranferenciaBodega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranferencia_bodega tranferencia_bodega = db.tranferencia_bodega.Find(id);
            if (tranferencia_bodega == null)
            {
                return HttpNotFound();
            }
            return View(tranferencia_bodega);
        }

        // POST: TranferenciaBodega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tranferencia_bodega tranferencia_bodega = db.tranferencia_bodega.Find(id);
            db.tranferencia_bodega.Remove(tranferencia_bodega);
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
