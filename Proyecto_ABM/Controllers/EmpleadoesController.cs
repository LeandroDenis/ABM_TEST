using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Proyecto_ABM.WebSite.Models;
using System;

namespace Proyecto_ABM.WebSite.Controllers
{
    [Authorize]
    public class EmpleadoesController : Controller
    {
        private EmpleadosDbContext db = new EmpleadosDbContext();

        public Empleado getEmpleadoById(int id)
        {
             return db.Empleados.Find(id);
        }

        // GET: Empleadoes
        public ActionResult Index()
        {
            return View(db.Empleados.ToList());
        }

        // GET: Empleadoes/Details/5
        [Authorize(Roles = "Admin_Detalle")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DetailsPartial", empleado);
        }

        // POST: Empleadoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin_Alta")]
        public ActionResult Create([Bind(Include = "Usuario,Nombre,Edad,Telefono,Observaciones")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Empleados.Add(empleado);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index", "Empleadoes");
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        ModelState.AddModelError("Error creando usuario", ex);
                    }
                }
            }
            return RedirectToAction("Index", "Empleadoes");
        }

        // GET: Empleadoes/Edit/5
        [Authorize(Roles = "Admin_Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditPartial", empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin_Edit")]
        public ActionResult Edit([Bind(Include = "Id,Usuario,Nombre,Edad,Telefono,Observaciones")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Entry(empleado).State = EntityState.Modified;
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        ModelState.AddModelError("Error editando usuario", ex);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Empleadoes/Delete/5
        [Authorize(Roles = "Admin_Baja")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeletePartial", empleado);
        }

        // POST: Empleadoes/Delete/5
        [Authorize(Roles = "Admin_Baja")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    Empleado empleado = db.Empleados.Find(id);
                    db.Empleados.Remove(empleado);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    ModelState.AddModelError("Error borrando usuario", ex);
                }
                return RedirectToAction("Index");
            }
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
