using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Acceso;
using Entidad;
using Negocio;

namespace API.Controllers
{
    [ApiController]
    [Route("api/transaccion")]

    public class TransaccionsController : ControllerBase
    {
        private readonly TransaccionService _service;

        public TransaccionsController(TransaccionService service)
        {
            _service = service;
        }



        /* CRUD -------------------------------------------------------------------------- */

        [HttpGet(Name = "ReadTransaccion")]
        public async Task<IActionResult> Read()
        {
            return Ok(await _service.GetTransaccionsAsync());
        }

        [HttpGet("customer/{id}",Name = "AllTransactionsByCustomer")]
        public async Task<IActionResult> GetAllByCustomer(int id)
        {
            return Ok(await _service.GetTransaccionsByIdAsync(id));
        }

        [HttpGet("{id}", Name = "DetailsTransaccion")]
        public async Task<IActionResult> Details(int? id)
        {
            var transaction = await _service.DetailsTransaccionsAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost(Name = "CreateTransaccion")]
        public async Task<IActionResult> Create(Transaccion transaccion)
        {
            return Ok(await _service.CreateTransaccionsAsync(transaccion));
        }

        [HttpPut("{id}", Name = "EditTransaccion")]
        public async Task<IActionResult> Edit(int id, Transaccion transaccion)
        {
            return Ok(await _service.EditTransaccionsAsync(id, transaccion));
        }

        [HttpDelete("{id}", Name = "DeleteTransaccion")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return Ok(await _service.DeleteTransaccionAsync(id));
        }

    }
}
