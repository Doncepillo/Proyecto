using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class SalasController : Controller
    {
        public ActionResult Salas()
        {
            return View();
        }

        public ActionResult GetSalas()
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var sala = dc.Salon.OrderBy(a => a.Sala).ToList();
                return Json(new { data = sala }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Salon.Where(a => a.Id == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public ActionResult Save(Salon s)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ProyectoEntities dc = new ProyectoEntities())
                {
                    if (s.Id > 0)
                    {

                        var v = dc.Salon.Where(a => a.Id == s.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.Sala = s.Sala;
                            v.Edificio = s.Edificio;
                           
                        }
                    }
                    else
                    {

                        dc.Salon.Add(s);
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
                var v = dc.Salon.Where(a => a.Id == id).FirstOrDefault();
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
        public ActionResult EliminarSala(int id)
        {
            bool status = false;
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Salon.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Salon.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}