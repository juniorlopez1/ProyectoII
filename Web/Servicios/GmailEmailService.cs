using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Servicios
{
    public class GmailEmailService : IEmailService
    {
        private readonly string host;
        private readonly string usuario;
        private readonly string contrasena;

        /* GMAIL --------------------------------------------------------------------------------------- */

        public GmailEmailService(string host, string usuario, string contrasena)
        {
            this.host = host;
            this.usuario = usuario;
            this.contrasena = contrasena;
        }

        public bool Enviar(string destinatario, string asunto, bool esHtml, string contenido)
        {
            try
            {
                using var mensaje = new MailMessage(usuario, destinatario)
                {
                    Subject = asunto,
                    Body = contenido,
                    IsBodyHtml = esHtml
                };

                using var smtp = new SmtpClient(host, 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                var NetworkCred = new NetworkCredential(usuario, contrasena);
                smtp.Credentials = NetworkCred;
                //https://myaccount.google.com/lesssecureapps?pli=1&rapt=AEjHL4OFgHsbJGHkd-MEUvemdK0kFQF2ShvNmPTeBuSu-UjsVVfqRd2mODY0I1IZIdE1NA2m1w-Ya6UxvBrgeqMpBsAxVHPJFg
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
