using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CambiarContrasenaViewModel
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string NuevaContrasena { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string ConfirmacionContrasena { get; set; }
    }
}
