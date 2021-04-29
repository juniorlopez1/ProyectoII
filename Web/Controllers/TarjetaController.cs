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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class TarjetaController : Controller
    {
        private readonly ITarjetaService _service;
        /* servicio de mongo */
        private readonly IBitacoraWebService _bitacoraService;
        public TarjetaController(ITarjetaService service, IBitacoraWebService bitacoraService)
        {
            _service = service;
            _bitacoraService = bitacoraService;
        }


        /* CRUD --------------------------------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var permiso = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            List<TarjetaViewModel> tarjetas;

            if (permiso == "Admin")
            {
                tarjetas = await _service.GetAllAsync();
            }
            else
            {
                tarjetas = await _service.GetByClienteIdAsync(id);
            }

            return View(tarjetas);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            TarjetaViewModel view = null;

            if (id == 0)
            {
                view = new TarjetaViewModel()
                {
                    Expiracion = DateTime.Now.Date
                };
            }
            else
            {
                view = await _service.GetByIdAsync(id);
            }

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Tarjeta",
                Accion = "Editar",
                Detalle = id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */


            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(TarjetaViewModel entidad)
        {

            if (!ModelState.IsValid)
            {
                return View("Editar", entidad);
            }
            if (entidad.Id == 0)
            {
                await _service.CreateAsync(entidad);
            }
            else
            {
                await _service.EditAsync(entidad);
            }

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Tarjeta",
                Accion = "Crear/Editar",
                Detalle = entidad.Id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return RedirectToAction("Lista");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            /* probado. Funciona */
            await _service.DeleteAsync(id);

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Tarjeta",
                Accion = "Eliminar",
                Detalle = id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return RedirectToAction("Lista");
        }

    }
}
