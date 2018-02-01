using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class AlumnosController : Controller
    {
       


        public ActionResult Alumnos()
        {
            ProyectoEntities proyectoentities = new ProyectoEntities();
            var getcursoslist = proyectoentities.Curso.ToList();
            SelectList list = new SelectList(getcursoslist, "Id", "Nombre");
            ViewBag.listacursosnombre = list;



            return View();
        }


        public ActionResult GetAlumnos()
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var alumno = dc.Alumno.OrderBy(a => a.Nombre).ToList();
                return Json(new { data = alumno }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Alumno.Where(a => a.Id == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public ActionResult Save(Alumno p)

        {
            
                
            

            bool status = false;
            if (ModelState.IsValid)
            {
                using (ProyectoEntities dc = new ProyectoEntities())
                {
                    if (p.Id > 0)
                    {
                        var v = dc.Alumno.Where(a => a.Id == p.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.Rut = p.Rut;
                            v.Nombre = p.Nombre;
                            v.Apellidos = p.Apellidos;
                            v.Curso = p.Curso;
                            v.Sexo = p.Sexo;
                            v.Telefono = p.Telefono;
                            v.Email = p.Email;
                        }
                    }
                    else
                    {
                        dc.Alumno.Add(p);
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
                var v = dc.Alumno.Where(a => a.Id == id).FirstOrDefault();
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
        public ActionResult EliminarAlumno(int id)
        {
            bool status = false;
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Alumno.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Alumno.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}