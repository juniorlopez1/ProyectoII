using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Models;
using Web.Servicios;

namespace Web.Controllers
{
    public class RestablecimientoController : Controller
    {
        private readonly IRestablecimientoService restablecimientoServicio;
        private readonly IClienteService clienteServicio;

        public RestablecimientoController(IRestablecimientoService restablecimientoServicio, IClienteService clienteServicio)
        {
            this.restablecimientoServicio = restablecimientoServicio;
            this.clienteServicio = clienteServicio;
        }

        /* PASSWORD ---------------------------------------------------------------------------------------------- */

        [HttpGet("restablecimiento/restablecer/{id}", Name = "RestablecerContrasena")]
        public async Task<IActionResult> Restablecer(string id)
        {
            var restablecimiento = await restablecimientoServicio.GetByIDAsync(id);

            RestablecimientoContrasenaViewModel cambioContrasena;
            if (restablecimiento == null)
            {
                cambioContrasena = new RestablecimientoContrasenaViewModel()
                {
                    IdRestablecimiento = string.Empty,
                    ConfirmacionContrasena = string.Empty,
                    NuevaContrasena = string.Empty,
                };
            }
            else
            {
                cambioContrasena = new RestablecimientoContrasenaViewModel()
                {
                    IdRestablecimiento = id,
                    ConfirmacionContrasena = string.Empty,
                    NuevaContrasena = string.Empty,
                };
            }

            return View(cambioContrasena);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Correo = string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(string correo)
        {
            var cliente = await clienteServicio.GetByNombreUsuarioAsync(correo);
            ViewBag.Correo = correo;

            if (cliente == null)
            {
                ModelState.AddModelError("", "La direccion de correo no es no existe en el sistema");
                return View("Restablecer");
            }
            else
            {
                var id = Guid.NewGuid().ToString();
                await restablecimientoServicio.Crear(new RestablecimientoViewModel()
                {
                    Id = id,
                    ClienteId = cliente.Id
                });

                await restablecimientoServicio.EnviarRestablecimiento(correo, id);
                return View("Confirmacion");
            }
        }

        [HttpGet]
        public IActionResult Confirmacion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(RestablecimientoContrasenaViewModel cambio)
        {
            if (!ModelState.IsValid)
            {
                return View("Restablecer", cambio);
            }

            var restablecimiento = await restablecimientoServicio.GetByIDAsync(cambio.IdRestablecimiento);

            if (restablecimiento == null)
            {
                ModelState.AddModelError("", "Ocurrio un problema al restablecer la contrasena");
                return View("Restablecer", cambio);
            }
            else
            {
                if (string.IsNullOrEmpty(cambio.NuevaContrasena.Trim()) || string.IsNullOrEmpty(cambio.ConfirmacionContrasena.Trim()))
                {
                    ModelState.AddModelError("", "La contrasena es requerida");
                    return View("Restablecer", cambio);
                }

                if (!cambio.NuevaContrasena.Trim().Equals(cambio.ConfirmacionContrasena.Trim()))
                {
                    ModelState.AddModelError("", "Las contrasenas no coinciden");
                    return View("Restablecer", cambio);
                }

                var cliente = await clienteServicio.GetByIdAsync(restablecimiento.ClienteId);
                cliente.Contrasena = cambio.NuevaContrasena;
                await clienteServicio.EditAsync(cliente);

                if (cliente == null)
                {
                    ModelState.AddModelError("", "Ocurrio un problema al restablecer la contrasena");
                    return View("Restablecer", cambio);
                }

                await restablecimientoServicio.Eliminar(cambio.IdRestablecimiento);

                return RedirectToAction("Login", "Cliente");
            }
        }
    }
}
