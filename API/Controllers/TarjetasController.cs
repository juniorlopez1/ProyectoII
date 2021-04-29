using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/tarjeta")]

    public class TarjetasController : ControllerBase
    {
        private readonly TarjetaService _service;

        public TarjetasController(TarjetaService service)
        {
            _service = service;
        }


        /* CRUD ------------------------------------------------------------------------ */

        [HttpGet(Name = "ReadTarjeta")]
        public async Task<IActionResult> Read()
        {
            return Ok(await _service.GetTarjetasAsync());
        }

        [HttpGet("cliente/{id}",Name = "GetByClienteId")]
        public async Task<IActionResult> GetByClienteId(int id)
        {
            return Ok(await _service.GetByClienteIdAsync(id));
        }

        [HttpGet("{id}", Name = "DetailsTarjeta")]
        public async Task<IActionResult> Details(int? id)
        {
            var tarjeta = await _service.DetailsTarjetasAsync(id);

            if (tarjeta == null)
            {
                return NotFound();
            }

            return Ok(tarjeta);
        }

        [HttpPost(Name = "CreateTarjeta")]
        public async Task<IActionResult> Create(Tarjeta tarjeta)
        {
            return Ok(await _service.CreateTarjetasAsync(tarjeta));
        }

        [HttpPut("{id}", Name = "EditTarjeta")]
        public async Task<IActionResult> Edit(int id, Tarjeta tarjeta)
        {
            await _service.EditTarjetaAsync(id, tarjeta);
            return CreatedAtRoute("DetailsTarjeta", new { id = tarjeta.Id }, tarjeta);
        }

        [HttpDelete("{id}", Name = "DeleteTarjeta")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return Ok(await _service.DeleteTarjetasAsync(id));
        }

    }
}
