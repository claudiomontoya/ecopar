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
    public class DestinosController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Destinos
        public ActionResult Index()
        {
            var destinos = db.destinos.Include(d => d.comunas);
            return View(destinos.ToList());
        }

        public JsonResult GetDestinos(string nombre)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<destinos> destinoListado = new List<destinos>();
            if (nombre != "")
                destinoListado = db.destinos.Where(c => c.nombre.Contains(nombre)).Take(10).ToList();
            else
                destinoListado = db.destinos.Take(10).ToList();

            return Json(destinoListado, JsonRequestBehavior.AllowGet);
        }

        // GET: Destinos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destinos destinos = db.destinos.Find(id);
            if (destinos == null)
            {
                return HttpNotFound();
            }
            return View(destinos);
        }

        // GET: Destinos/Create
        public ActionResult Create()
        {
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            var reg = db.regiones.FirstOrDefault();
            ViewBag.id_comuna = new SelectList(db.comunas.Where(x => x.id_region == reg.id).ToList(), "id", "comuna");
           
            return View();
        }

        // POST: Destinos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,direccion,id_comuna,telefono,mail")] destinos destinos)
        {
            if (ModelState.IsValid)
            {
                db.destinos.Add(destinos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", destinos.id_comuna);
            return View(destinos);
        }

        // GET: Destinos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destinos destinos = db.destinos.Find(id);
            if (destinos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", destinos.id_comuna);
            return View(destinos);
        }

        // POST: Destinos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,direccion,id_comuna,telefono,mail")] destinos destinos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destinos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", destinos.id_comuna);
            return View(destinos);
        }

        // GET: Destinos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destinos destinos = db.destinos.Find(id);
            if (destinos == null)
            {
                return HttpNotFound();
            }
            return View(destinos);
        }

        // POST: Destinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            destinos destinos = db.destinos.Find(id);
            db.destinos.Remove(destinos);
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
