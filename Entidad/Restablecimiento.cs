using System;
using System.Collections.Generic;

#nullable disable

namespace Entidad
{
    public partial class Restablecimiento
    {
        /* Propiedades ----------------------------------------------------------------------------- */

        public string Id { get; set; }
        public int ClienteId { get; set; }


        /* Otras entidades ----------------------------------------------------------------------------- */

        public virtual Cliente Cliente { get; set; }

    }
}
