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
    public class TranferenciaDestinoController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: TranferenciaDestino
        public ActionResult Index()
        {
            var tranferencia_destino = db.tranferencia_destino.Include(t => t.bodegasdestinos).Include(t => t.emision);
            return View(tranferencia_destino.ToList());
        }

        // GET: TranferenciaDestino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranferencia_destino tranferencia_destino = db.tranferencia_destino.Find(id);
            if (tranferencia_destino == null)
            {
                return HttpNotFound();
            }
            return View(tranferencia_destino);
        }

        // GET: TranferenciaDestino/Create
        public ActionResult Create(int id)
        {
            var des = db.ClienteDestino.Where(x=>x.id_cliente==1).ToList();
            List<int> dest = new List<int>();

            foreach (var item in des)
            {
                dest.Add(item.id_destino);
            }
            ViewBag.destinos = new SelectList(db.destinos.Where(x => dest.Any(y => y == x.id)).ToList(), "id", "nombre");
            var destin = db.destinos.Where(x => dest.Any(y => y == x.id)).FirstOrDefault().id;
            ViewBag.cantidad = db.emision.Where(x => x.id == id).FirstOrDefault().cantidad;
            ViewBag.id_destino = new SelectList(db.bodegasdestinos.Where(x => x.id_destino == destin).ToList(), "id", "descripcion");
            ViewBag.id_emision =id;
            return View();
        }

        // POST: TranferenciaDestino/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_emision,id_destino,fecha_creacion,usuario,archivo,fecha_retiro,cantidad,estado,fecha_envio,tipo")] tranferencia_destino tranferencia_destino)
        {

            tranferencia_destino.usuario = "Admin";
            tranferencia_destino.fecha_creacion = DateTime.Now;
            tranferencia_destino.fecha_retiro = DateTime.Now;
            tranferencia_destino.fecha_envio = DateTime.Now;
            tranferencia_destino.estado = "Iniciada";
            tranferencia_destino.archivo = "";

            if (true)
            {

                db.tranferencia_destino.Add(tranferencia_destino);
                db.SaveChanges();

                emision emi = db.emision.Where(x => x.id == tranferencia_destino.id_emision).FirstOrDefault();
                emi.estado = "Transferida a Destino";
                db.Entry(emi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     

            ViewBag.id_destino = new SelectList(db.bodegasdestinos, "id", "descripcion", tranferencia_destino.id_destino);
            ViewBag.id_emision = new SelectList(db.emision, "id", "usuarioeco", tranferencia_destino.id_emision);
            return View(tranferencia_destino);
        }

        // GET: TranferenciaDestino/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranferencia_destino tranferencia_destino = db.tranferencia_destino.Find(id);
            if (tranferencia_destino == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_destino = new SelectList(db.bodegasdestinos, "id", "descripcion", tranferencia_destino.id_destino);
            ViewBag.id_emision = new SelectList(db.emision, "id", "usuarioeco", tranferencia_destino.id_emision);
            return View(tranferencia_destino);
        }

        // POST: TranferenciaDestino/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_emision,id_destino,fecha_creacion,usuario,archivo,fecha_retiro,cantidad,estado,fecha_envio,tipo")] tranferencia_destino tranferencia_destino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tranferencia_destino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_destino = new SelectList(db.bodegasdestinos, "id", "descripcion", tranferencia_destino.id_destino);
            ViewBag.id_emision = new SelectList(db.emision, "id", "usuarioeco", tranferencia_destino.id_emision);
            return View(tranferencia_destino);
        }

        // GET: TranferenciaDestino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranferencia_destino tranferencia_destino = db.tranferencia_destino.Find(id);
            if (tranferencia_destino == null)
            {
                return HttpNotFound();
            }
            return View(tranferencia_destino);
        }

        // POST: TranferenciaDestino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tranferencia_destino tranferencia_destino = db.tranferencia_destino.Find(id);
            db.tranferencia_destino.Remove(tranferencia_destino);
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

        public JsonResult GetBodega(int destino)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<bodegasdestinos> bodegaListado = db.bodegasdestinos.Where(x => x.id_destino== destino).ToList();

            return Json(bodegaListado, JsonRequestBehavior.AllowGet);
        }
    }
}
