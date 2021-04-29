using System;
using System.Collections.Generic;

#nullable disable

namespace Entidad
{
    public partial class Tarjeta
    {
        /* Constructor ----------------------------------------------------------------------------- */

        public Tarjeta()
        {
            Transaccions = new HashSet<Transaccion>();
        }


        /* Propiedades ----------------------------------------------------------------------------- */

        public int Id { get; set; }
        public long NumeroTarjeta { get; set; }
        public int ClienteId { get; set; }
        public DateTime Expiracion { get; set; }
        public string Titular { get; set; }
        public short Csv { get; set; }
        public string TipoTarjeta { get; set; }


        /* Otras entidades ----------------------------------------------------------------------------- */

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Transaccion> Transaccions { get; set; }

    }
}
