using System;
using System.Collections.Generic;

#nullable disable

namespace Entidad
{
    public partial class Transaccion
    {
        /* Propiedades ----------------------------------------------------------------------------- */

        public int Id { get; set; }
        public string Estado { get; set; }
        public int ClienteOrigen { get; set; }
        public int ClienteDestino { get; set; }
        public double Monto { get; set; }
        public int MetodoPagoId { get; set; }
        public string Detalle { get; set; }


        /* Otras entidades ----------------------------------------------------------------------------- */

        public virtual Cliente ClienteDestinoNavigation { get; set; }
        public virtual Cliente ClienteOrigenNavigation { get; set; }
        public virtual Tarjeta MetodoPago { get; set; }

    }
}
