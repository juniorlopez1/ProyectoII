using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Servicios
{
    public interface IRestablecimientoService
    {
        Task Crear(RestablecimientoViewModel restablecimiento);
        Task<RestablecimientoViewModel> GetByIDAsync(string id);
        Task Eliminar(string id);
        Task EnviarRestablecimiento(string direccion, string id);
    }

    public class RestablecimientoService : ServicioBase, IRestablecimientoService
    {
        private IEmailService _emailService;

        public RestablecimientoService(string baseUrl, IEmailService emailService) : base(baseUrl)
        {
            _emailService = emailService;
        }

        public async Task<RestablecimientoViewModel> GetByIDAsync(string id)
        {
            return await GetAsync<RestablecimientoViewModel>($"restablecimiento/{id}").ConfigureAwait(false);
        }

        public async Task Crear(RestablecimientoViewModel restablecimiento)
        {
            await PostAsync("restablecimiento", restablecimiento).ConfigureAwait(false);
        }

        public async Task Eliminar(string id)
        {
            await DeleteAsync($"restablecimiento/{id}").ConfigureAwait(false);
        }

        public async Task EnviarRestablecimiento(string direccion, string id)
        {
            var url = $"https://localhost:7001/restablecimiento/Restablecer/{id}";
            string mensaje = $"<a href=\"{url}\">Haga clic para restablecer su contrasena</a>";
            await Task.Factory.StartNew(() => _emailService.Enviar(direccion, "Restablecer contrasena", true, mensaje));
        }
    }
}
