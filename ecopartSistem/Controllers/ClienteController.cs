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
    public class ClienteController : Controller
    {
        private ecoparModel db = new ecoparModel();

        // GET: Cliente
        public ActionResult Index()
        {
            var clientes = db.clientes.Include(c => c.comunas).Include(c => c.formapago);
            return View(clientes.ToList());
        }

        public JsonResult GetClientes(string nombre)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<clientes> clienteListado = new List<clientes>();
            if (nombre!="")
            clienteListado = db.clientes.Where(c => c.nombre.Contains(nombre)).Take(10).ToList();
            else
            clienteListado = db.clientes.Take(10).ToList();

            return Json(clienteListado, JsonRequestBehavior.AllowGet);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clientes clientes = db.clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.encargadopago = db.Personas.Where(x => x.id == clientes.id_encargado_pago).FirstOrDefault();
            ViewBag.encargadocontrato = db.Personas.Where(x => x.id == clientes.id_encargado_contrato).FirstOrDefault();
            return View(clientes);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            var reg = db.regiones.FirstOrDefault();
            ViewBag.id_comuna = new SelectList(db.comunas.Where(x => x.id_region == reg.id).ToList(), "id", "comuna");
            ViewBag.id_formaPago = new SelectList(db.formapago, "id", "nombre");
            return View();
        }

        // POST: Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,rut,nombre,direccion,numero,id_comuna,id_formaPago,telefono,celular,correo,clave,id_encargado_pago,id_encargado_contrato,descripcion")] clientes clientes)
        {
            clientes.id_encargado_contrato = 0;
            clientes.id_encargado_pago = 0;
            if (ModelState.IsValid)
            {
                db.clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.region = new SelectList(db.regiones, "id", "region");
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", clientes.id_comuna);
            ViewBag.id_formaPago = new SelectList(db.formapago, "id", "nombre", clientes.id_formaPago);
            return View(clientes);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clientes clientes = db.clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", clientes.id_comuna);
            ViewBag.id_formaPago = new SelectList(db.formapago, "id", "nombre", clientes.id_formaPago);
            return View(clientes);
        }

        // POST: Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,rut,nombre,direccion,numero,id_comuna,id_formaPago,telefono,celular,correo,clave,id_encargado_pago,id_encargado_contrato,descripcion")] clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_comuna = new SelectList(db.comunas, "id", "comuna", clientes.id_comuna);
            ViewBag.id_formaPago = new SelectList(db.formapago, "id", "nombre", clientes.id_formaPago);
            return View(clientes);
        }

        public ActionResult AgregaEncargado(int id, int idEncargado,int tipo)
        {
            clientes cliente = db.clientes.Where(x => x.id == id).FirstOrDefault();
            if(tipo==1)
            {
                cliente.id_encargado_pago = idEncargado;
            }else
            {
                cliente.id_encargado_contrato = idEncargado;
            }
            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { @id = id });
        }


        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clientes clientes = db.clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            clientes clientes = db.clientes.Find(id);
            db.clientes.Remove(clientes);
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
