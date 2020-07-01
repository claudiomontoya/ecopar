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
    public class SucursalController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Sucursal
        public ActionResult Index()
        {
            var sucursal = db.sucursal.Include(s => s.comunas);
            return View(sucursal.ToList());
        }

        // GET: Sucursal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sucursal sucursal = db.sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // GET: Sucursal/Create
        public ActionResult Create()
        {
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            var reg = db.regiones.FirstOrDefault();
            ViewBag.id_comuna = new SelectList(db.comunas.Where(x=>x.id_region==reg.id).ToList(), "id", "comuna");
            
            return View();
        }
        public JsonResult GetComunas(int region)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<comunas> comunaListado = db.comunas.Where(x => x.id_region == region).ToList();

            return Json(comunaListado, JsonRequestBehavior.AllowGet);
        }

        // POST: Sucursal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,direccion,numero,telefono,celular,id_comuna")] sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.sucursal.Add(sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            ViewBag.id_comuna = new SelectList(db.comunas.Where(x => x.id == sucursal.id_comuna).ToList(), "id", "comuna");
            
            return View(sucursal);
        }

        // GET: Sucursal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sucursal sucursal = db.sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            ViewBag.id_comuna = new SelectList(db.comunas.Where(x=>x.id==sucursal.id_comuna).ToList(), "id", "comuna", sucursal.id_comuna);
            return View(sucursal);
        }

        // POST: Sucursal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,direccion,numero,telefono,celular,id_comuna")] sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", sucursal.id_comuna);
            return View(sucursal);
        }

        // GET: Sucursal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sucursal sucursal = db.sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sucursal sucursal = db.sucursal.Find(id);
            db.sucursal.Remove(sucursal);
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
