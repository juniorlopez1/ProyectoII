using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Servicios
{
    public class Smtp4devEmailService : IEmailService
    {
        private readonly string host;
        private readonly int port;
        private readonly string from;

        public Smtp4devEmailService(string host, string from, int port)
        {
            this.host = host;
            this.port = port;
            this.from = from;
        }

        public bool Enviar(string destinatario, string asunto, bool esHtml, string contenido)
        {
            try
            {
                using var mensaje = new MailMessage(from, destinatario)
                {
                    Subject = asunto,
                    Body = contenido,
                    IsBodyHtml = esHtml
                };

                using var smtp = new SmtpClient(host, port);         
                smtp.UseDefaultCredentials = true;
                smtp.Send(mensaje);
            
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}