using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class DocentesController : Controller
    {
        public ActionResult Docentes()
        {
            return View();
        }
        public ActionResult GetDocentes()
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var docente = dc.Docente.OrderBy(a => a.Nombre).ToList();
                return Json(new { data = docente }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Docente.Where(a => a.Id == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public ActionResult Save(Docente d)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ProyectoEntities dc = new ProyectoEntities())
                {
                    if (d.Id > 0)
                    {
                        var v = dc.Docente.Where(a => a.Id == d.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.Rut = d.Rut;
                            v.Nombre = d.Nombre;
                            v.Apellidos = d.Apellidos;
                            v.Sexo = d.Sexo;
                            v.Titulo = d.Titulo;
                            v.Telefono = d.Telefono;
                            v.Email = d.Email;
                        }
                    }
                    else
                    {
                        dc.Docente.Add(d);
                    }

                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Docente.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult EliminarDocente(int id)
        {
            bool status = false;
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Docente.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Docente.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}