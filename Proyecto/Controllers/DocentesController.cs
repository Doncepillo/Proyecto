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
        ProyectoEntities1 cnx;

        public DocentesController()
        {
            cnx = new ProyectoEntities1();

        }
        public ActionResult NuevoDocente()
        {

            return View();
        }

        public ActionResult Listado()
        {

            cnx.Database.Connection.Open();

            List<Docente> docente = cnx.Docente.ToList();
            cnx.Database.Connection.Close();
            return View(docente);
        }



        public ActionResult Guardar(string Rut, string Nombre, string Apellidos, string Sexo, string Titulo, int Telefono, string Email)
        {
            Docente docente = new Docente()
            {
                Rut = Rut,
                Nombre = Nombre,
                Apellidos = Apellidos,
                Sexo = Sexo,
                Titulo = Titulo,
                Telefono = Telefono,
                Email = Email

            };

            cnx.Docente.Add(docente);
            cnx.SaveChanges();
            return View("Listado", cnx.Docente.ToList());

        }



        public ActionResult Ficha(string Rut)
        {

            return View(cnx.Docente.Where(x => x.Rut == Rut).First());

        }




        public ActionResult Ver(string Rut)
        {


            return View("Ficha", null);
        }


        public ActionResult Eliminar(string Rut)
        {
            cnx.Docente.Remove(cnx.Docente.Where(x => x.Rut == Rut).First());
            cnx.SaveChanges();
            return View("Listado", cnx.Docente.ToList());

        }

        public ActionResult Editar(string Rut)
        {

            return View(cnx.Docente.Where(x => x.Rut == Rut).First());



        }


    }
}