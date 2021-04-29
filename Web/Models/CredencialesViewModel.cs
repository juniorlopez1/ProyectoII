using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CredencialesViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Contrasena { get; set; }
    }
}
