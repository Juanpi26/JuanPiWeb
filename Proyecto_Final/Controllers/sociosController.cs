using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class sociosController : Controller
    {
        private ProyectoFinalEntities db = new ProyectoFinalEntities();

        // GET: socios
        [Authorize]
        public ActionResult Index()
        {
            return View(db.socios.ToList());
        }

        // GET: socios/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            socios socios = db.socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // GET: socios/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: socios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Apellido,Cedula,Foto,Direccion,Telefono,Sexo,Edad,Fecha_de_nacimiento,Afiliados,Datos_Membresia,Lugar_de_trabajo,Direccion_Oficina,Telefono_Oficina,Estado_Membresia,Fecha_de_ingreso,Fecha_de_salida")] socios socios, HttpPostedFileBase archivo)
        {

            string nombreARchivo = Path.GetFileNameWithoutExtension(archivo.FileName);
            string extension = Path.GetExtension(archivo.FileName);
            nombreARchivo = nombreARchivo + DateTime.Now.ToString("yymmssfff") + extension;

            socios.Foto = "~/images/" + nombreARchivo;
            nombreARchivo = Path.Combine(Server.MapPath("~/images"), nombreARchivo);
            archivo.SaveAs(nombreARchivo);

            if (ModelState.IsValid)
            {
                db.socios.Add(socios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socios);
        }

        // GET: socios/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            socios socios = db.socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // POST: socios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Apellido,Cedula,Foto,Direccion,Telefono,Sexo,Edad,Fecha_de_nacimiento,Afiliados,Datos_Membresia,Lugar_de_trabajo,Direccion_Oficina,Telefono_Oficina,Estado_Membresia,Fecha_de_ingreso,Fecha_de_salida")] socios socios, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                string nombreARchivo = Path.GetFileNameWithoutExtension(imagen.FileName);
                string extension = Path.GetExtension(imagen.FileName);
                nombreARchivo = nombreARchivo + DateTime.Now.ToString("yymmssfff") + extension;
                socios.Foto = "~/images/" + nombreARchivo;
                nombreARchivo = Path.Combine(Server.MapPath("~/images"), nombreARchivo);
                imagen.SaveAs(nombreARchivo);


                db.Entry(socios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socios);
        }

        // GET: socios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            socios socios = db.socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // POST: socios/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            socios socios = db.socios.Find(id);
            db.socios.Remove(socios);
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
