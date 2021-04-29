using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /* Este es el controller */

    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {
        #region Property - Constructors

        private readonly IBitacoraService _bitacoraService;

        public BitacoraController(IBitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        public ActionResult<List<Bitacora>> Get() =>
            _bitacoraService.Get();

        [HttpPost]
        public ActionResult<Bitacora> Create(Bitacora registro)
        {
            _bitacoraService.Create(registro);

            return CreatedAtRoute("GetRegistro", new { id = registro.Id.ToString() }, registro);
        }

        #endregion

        #region CRUD Unused

        //[HttpGet("{id:length(24)}", Name = "GetRegistro")]
        //public ActionResult<Bitacora> Get(string id)
        //{
        //    var book = _bitacoraService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Bitacora registroIn)
        //{
        //    var book = _bitacoraService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bitacoraService.Update(id, registroIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var registro = _bitacoraService.Get(id);

        //    if (registro == null)
        //    {
        //        return NotFound();
        //    }

        //    _bitacoraService.Remove(registro.Id);

        //    return NoContent();
        //}

        #endregion
    }
}
