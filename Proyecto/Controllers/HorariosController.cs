using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class HorariosController : Controller
    {
        public ActionResult Horario()
        {
            return View();
        }


        public JsonResult GetEvents()
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var events = dc.Evento.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetEventsFilter(string tipo, string value)
        {
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                List<Evento> events;

                switch (tipo)
                {
                    case "ThemeColor":
                        events = dc.Evento.Where(x => x.ThemeColor == value).ToList();
                        break;
                    case "Subject":
                        events = dc.Evento.Where(x => x.Subject == value).ToList();
                        break;
                    case "Docente":
                        events = dc.Evento.Where(x => x.Docente == value).ToList();
                        break;
                    case "Sala":
                        events = dc.Evento.Where(x => x.Sala == value).ToList();
                        break;
                    case "Edificio":
                        events = dc.Evento.Where(x => x.Edificio == value).ToList();
                        break;

                    default:
                        events = dc.Evento.ToList();
                        break;
                }
                if (tipo.Equals("ThemeColor"))
                {

                }
                if (tipo.Equals("Subject"))
                {

                }
                if (tipo.Equals("Docente"))
                {

                }
                if (tipo.Equals("Sala"))
                {

                }
                if (tipo.Equals("Edificio"))
                {

                }

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        public void EnviarMail(int EventId, string tipoEvento, string Nombre) {

            //con el id de evento, seleccional el curso.
            //con el curso, seleccionas los alumnos de ese curso.
            //lamar a metodo buscar Alumnos()
            // (para) Alumnos listado = resultado ....

            // (de) Profe
            // (Asunto) tipo de mensaje ( elimina clase, cambia horario, cambia sala)

            //cuepor mensaje


            //llamar

            Services.SendMail mail = new Services.SendMail();

            string[] para = new string[] { "jackdaniels@gmail.com", "daniel.perezz@correoaiep.cl" };
            mail.Send(para, "sistemaSalas@adminprofe.cl", "Correo de Aviso", "<h1> Hola Mundo</h1><hr/> <p> esto es way</p>");
        }
        [HttpPost]
        public JsonResult SaveEvent(Evento e)
        {
            var status = false;
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                if (e.EventId > 0)
                {

                    var v = dc.Evento.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Docente = e.Docente;
                        v.Sala = e.Sala;
                        v.Edificio = e.Edificio;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Evento.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ProyectoEntities dc = new ProyectoEntities())
            {
                var v = dc.Evento.Where(a => a.EventId == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Evento.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}