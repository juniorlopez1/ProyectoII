using System;

namespace Web.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public DateTime ExpiracionContrasena { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; } //!
        public string TipoCliente { get; set; }
        public bool Estado { get; set; }
        public string Permiso { get; set; }

        //Otras entidades
        //public RestablecimientoViewModel Restablecimientos { get; set; }
        //public TarjetaViewModel Tarjeta { get; set; }
        //public TransaccionViewModel TransaccionClienteDestinoNavigations { get; set; }
        //public TransaccionViewModel TransaccionClienteOrigenNavigations { get; set; }
    }
}
