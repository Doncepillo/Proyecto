using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Proyecto.Services
{
    public class SendMail
    {
         
        private string from;
        private string password;
        private string server;
        private string account;
        private int port;

        public SendMail() {
            this.account = "danieleperezzuniga@gmail.com";
            this.password = "lavoragine";
            this.server = "smtp.gmail.com"; 
             
        }

        public void Send(string[] to, string from, string subject, string body) {
            MailMessage mail = new MailMessage();
            foreach (string email in to) {
                mail.To.Add(email); 
            }

            mail.From = new MailAddress(from);
            mail.Subject = subject;
             
            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = this.server;
            smtp.Credentials = new System.Net.NetworkCredential
                 (this.account, this.password);

            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
      
    }
}