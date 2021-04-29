using System;
using System.Collections.Generic;

#nullable disable

namespace Entidad
{
    public partial class Cliente
    {
        /* Constructor ----------------------------------------------------------------------------- */

        public Cliente()
        {
            Restablecimientos = new HashSet<Restablecimiento>();
            Tarjeta = new HashSet<Tarjeta>();
            TransaccionClienteDestinoNavigations = new HashSet<Transaccion>();
            TransaccionClienteOrigenNavigations = new HashSet<Transaccion>();
        }


        /* Propiedades ----------------------------------------------------------------------------- */

        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Contrasena { get; set; }
        public DateTime ExpiracionContrasena { get; set; }
        public string TipoCliente { get; set; }
        public bool Estado { get; set; }
        public string Permiso { get; set; }

        /* Otras entidades ----------------------------------------------------------------------------- */

        public virtual ICollection<Restablecimiento> Restablecimientos { get; set; }
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
        public virtual ICollection<Transaccion> TransaccionClienteDestinoNavigations { get; set; }
        public virtual ICollection<Transaccion> TransaccionClienteOrigenNavigations { get; set; }

    }
}
