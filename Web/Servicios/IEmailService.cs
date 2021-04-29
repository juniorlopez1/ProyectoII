using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Servicios
{
    public interface IEmailService
    {
        bool Enviar(string destinatario, string asunto, bool esHtml, string contenido);
    }
}
