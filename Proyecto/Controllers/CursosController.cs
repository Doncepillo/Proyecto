using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CursosController : Controller
    {
        public ActionResult Cursos()
        {
            return View();
        }

        public ActionResult GetCursos()
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var curso = dc.Curso.OrderBy(a => a.Nombre).ToList();
                return Json(new { data = curso }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Curso.Where(a => a.Id == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public ActionResult Save(Curso c)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ProyectoEntities dc = new ProyectoEntities())
                {
                    if (c.Id > 0)
                    {
                        
                        var v = dc.Curso.Where(a => a.Id == c.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.Nombre = c.Nombre;
                            v.Docente = c.Docente;
                            v.Jornada = c.Jornada;
                            v.Modalidad = c.Modalidad;
                        }
                    }
                    else
                    {
                        
                        dc.Curso.Add(c);
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
                var v = dc.Curso.Where(a => a.Id == id).FirstOrDefault();
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
        public ActionResult EliminarCurso(int id)
        {
            bool status = false;
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Curso.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Curso.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}