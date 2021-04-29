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
    public class TransaccionController : Controller
    {
        private readonly ITransaccionService _service;
        private readonly IBitacoraWebService _bitacoraService;

        public TransaccionController(ITransaccionService service, IBitacoraWebService bitacoraservice)
        {
            _service = service;
            _bitacoraService = bitacoraservice;
        }

        /* CRUD -------------------------------------------------------------------------------- */

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var permiso = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            List<TransaccionViewModel> transactiones;

            if (permiso.Equals("Admin"))
            {
                transactiones = await _service.GetAllAsync();
            }
            else
            {
                transactiones = await _service.GetAllByCustomerIdAsync(id);
            }

            return View(transactiones);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            TransaccionViewModel view = null;

            if (id == 0)
            {
                view = new TransaccionViewModel();
            }
            else
            {
                view = await _service.GetByIdAsync(id);
            }

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Transaccion",
                Accion = "Editar",
                Detalle = id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(TransaccionViewModel entidad)
        {

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
                Entidad = "Transaccion",
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
            await _service.DeleteAsync(id);

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                fecha = DateTime.Now,
                Entidad = "Transaccion",
                Accion = "Eliminar",
                Detalle = id.ToString()
            };
            await _bitacoraService.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return RedirectToAction("Lista");
        }

    }
}
