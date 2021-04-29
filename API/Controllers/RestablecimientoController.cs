using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/restablecimiento")]
    public class RestablecimientoController : ControllerBase
    {
        private readonly RestablecimientoService _service;

        public RestablecimientoController(RestablecimientoService service)
        {
            _service = service;
        }

        /* PASSWORD ------------------------------------------------------------------------------ */

        [HttpPost(Name = "CreateRestablecimiento")]
        public async Task<IActionResult> Create(Restablecimiento entidad)
        {
            await _service.CreateAsync(entidad);
            return CreatedAtRoute(nameof(Search), new { id = entidad.Id }, entidad);
        }


        [HttpGet("{id}", Name = "SearchRestablecimiento")]
        public async Task<IActionResult> Search(string id)
        {
            var entidad = await _service.GetByIDAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(entidad);
            }
        }


        [HttpDelete("{id}", Name = "EliminarRestablecimiento")]
        public async Task<IActionResult> Eiminar(string id)
        {
            await _service.DeleteByIDAsync(id);
            return NoContent();
        }
    }
}
