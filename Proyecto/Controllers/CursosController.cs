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
        ProyectoEntities1 cnx;

        public CursosController()
        {
            cnx = new ProyectoEntities1();

        }
        public ActionResult NuevoCurso()
        {

            return View();
        }


        public ActionResult Listado()
        {

            cnx.Database.Connection.Open();

            List<Curso> curso = cnx.Curso.ToList();
            cnx.Database.Connection.Close();
            return View(curso);
        }

        

        public ActionResult Guardar(string Nombre, string Docente, int Costo, string Jornada, string Modalidad, int Cupo)
        {
            Curso curso = new Curso()
            {
               
                Nombre = Nombre,
                Docente = Docente,
                Costo = Costo,
                Jornada = Jornada,
                Modalidad = Modalidad,
                Cupo = Cupo

            };

            cnx.Curso.Add(curso);
            cnx.SaveChanges();
            return View("Listado", cnx.Curso.ToList());

        }



        public ActionResult Ficha(int Id)
        {

            return View(cnx.Curso.Where(x => x.Id == Id).First());

        }




        public ActionResult Ver(int Id)
        {


            return View("Ficha", null);
        }


        public ActionResult Eliminar(int Id)
        {
            cnx.Curso.Remove(cnx.Curso.Where(x => x.Id == Id).First());
            cnx.SaveChanges();
            return View("Listado", cnx.Curso.ToList());

        }

        public ActionResult Editar(int Id)
        {

            return View(cnx.Curso.Where(x => x.Id == Id).First());



        }


    }
}