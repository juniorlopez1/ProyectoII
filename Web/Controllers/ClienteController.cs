
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;
using Web.Servicios;

namespace Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;
        /* servicio de mongo */
        private readonly IBitacoraWebService _bitacoraService;

        public ClienteController(IClienteService service, IBitacoraWebService bitacoraService)
        {
            _service = service;
            _bitacoraService = bitacoraService;
        }


        /* LOGIN ---------------------------------------------------------------------------------------------- */
        [HttpGet]
        public IActionResult Login()
        {
            var credenciales = new CredencialesViewModel();
            return View(credenciales);
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(CredencialesViewModel credenciales)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", credenciales);
            }
            var cliente = await _service.AutenticarAsync(credenciales);
            if (cliente == null)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña invalidos");
                return View("Login", credenciales);
            }
            else
            {
                if (cliente.ExpiracionContrasena < DateTime.Now.Date)
                {
                    return RedirectToAction("CambiarContrasena", "Cliente", new { id = cliente.Id });
                }

                var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Role, cliente.Permiso),
                        new Claim(ClaimTypes.Email, cliente.NombreUsuario),
                        new Claim("Id", cliente.Id.ToString()),
                    }, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }



        /* CRUD ---------------------------------------------------------------------------------------------- */

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Lista()
        {
            var permiso = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            List<ClienteViewModel> clientes;

            if (permiso == "Admin")
            {
                clientes = await _service.GetAllAsync();
            }
            else
            {
                var cliente = await _service.GetByIdAsync(id);
                clientes = new List<ClienteViewModel>();
                clientes.Add(cliente);
            }

            return View(clientes);
        }


        [HttpGet("Editar/{id?}")]
        public async Task<IActionResult> Editar(int id)
        {
            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Cliente",
                Accion = "Editar",
                Detalle = id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            var cliente = id == 0 ? new ClienteViewModel() : await _service.GetByIdAsync(id);
            return View(cliente);
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Editar), cliente);
            }

            if (cliente.Id == 0)
            {

                await _service.CreateAsync(cliente);
            }
            else
            {
                await _service.EditAsync(cliente);
            }

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Cliente",
                Accion = "Editar/Crear",
                Detalle = cliente.Id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */



            return RedirectToAction(nameof(Lista));
        }


        [HttpGet("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Cliente",
                Accion = "Eliminar",
                Detalle = id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            await _service.DeleteAsync(id);
            return RedirectToAction("Lista");
        }

        [HttpGet("contrasena")]
        public IActionResult CambiarContrasena(int id)
        {
            var cambioContrasena = new CambiarContrasenaViewModel
            {
                IdUsuario = id
            };

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Cliente",
                Accion = "Cambio Contrasena",
                Detalle = id.ToString()
            };
            _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return View(cambioContrasena);

        }

        [HttpPost("contrasena")]
        public async Task<IActionResult> ActualizarContrasena(CambiarContrasenaViewModel cambioContrasena)
        {
            var cliente = await _service.GetByIdAsync(cambioContrasena.IdUsuario);

            if (cliente == null)
            {
                ModelState.AddModelError("", "Usuario invalido");
                return View(nameof(CambiarContrasena), cambioContrasena);
            }

            if (cambioContrasena.NuevaContrasena.Trim() != cambioContrasena.ConfirmacionContrasena.Trim())
            {
                ModelState.AddModelError("", "Las contrasenas deben coincidir");
                return View(nameof(CambiarContrasena), cambioContrasena);
            }

            if (cambioContrasena.NuevaContrasena.Trim() == cliente.Contrasena)
            {
                ModelState.AddModelError("", "La nueva contrasena no puede ser igual a la anterior");
                return View(nameof(CambiarContrasena), cambioContrasena);
            }

            if (!ModelState.IsValid)
            {
                return View(nameof(CambiarContrasena), cambioContrasena);
            }

            cliente.Contrasena = cambioContrasena.NuevaContrasena;
            cliente.ExpiracionContrasena = DateTime.Now.AddDays(30).Date;
            await _service.EditAsync(cliente);

            return RedirectToAction(nameof(Login));
        }

    }
}
