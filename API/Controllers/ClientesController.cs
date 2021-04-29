using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entidad;
using Acceso;
using Negocio;

namespace API.Controllers
{
    [ApiController]
    [Route("api/cliente")]

    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            _service = service;
        }


        /* CRUD ------------------------------------------------------------------------------ */

        [HttpGet(Name = "ReadCliente")]
        public async Task<IActionResult> Read()
        {
            return Ok(await _service.ReadClientesAsync());
        }

        [HttpGet("{id}", Name = "DetailsCliente")]
        public async Task<IActionResult> Details(int? id)
        {
            var cliente = await _service.DetailsClientesAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(await _service.DetailsClientesAsync(id));
        }

        [HttpPost(Name = "CreateCliente")]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateClientesAsync(cliente);

                return CreatedAtRoute("DetailsCliente", new { id = cliente.Id }, cliente);
            }
            return Ok(cliente);
        }

        [HttpPut("{id}", Name = "EditCliente")]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {

            await _service.EditClientesAsync(id, cliente);
            return CreatedAtRoute("DetailsCliente", new { id = cliente.Id }, cliente);
        }

        [HttpDelete("{id}", Name = "DeleteCliente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteClientesAsync(id);
            return NoContent();
        }



        /* LOGIN ------------------------------------------------------------------------------ */

        [HttpGet("nombre-usuario/{nombreUsuario}", Name = "GetByNombreUsuario")]
        public async Task<IActionResult> GetByNombreUsuario(string nombreUsuario)
        {
            var cliente = await _service.GetByNombreDeUsuarioAsync(nombreUsuario);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost("autenticar", Name = "Autenticar")]
        public async Task<IActionResult> Autenticar(Credenciales credenciales)
        {
            var usuario = await _service.GetByNombreUsuarioAndContrasenaAsync(credenciales.NombreUsuario, credenciales.Contrasena);

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuario);
            }
        }

    }
}
