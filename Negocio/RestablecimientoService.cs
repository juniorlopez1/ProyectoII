using Acceso;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Negocio
{
    public class RestablecimientoService
    {
        private readonly Context _context;

        public RestablecimientoService(Context context)
        {
            _context = context;
        }


        /* PASSWORD -------------------------------------------------------------------------------------- */
        public async Task<Restablecimiento> GetByIDAsync(string id)
        {
            return await _context.Restablecimiento.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Restablecimiento restablecimiento)
        {
            await _context.Restablecimiento.AddAsync(restablecimiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIDAsync(string id)
        {
            var restablecimiento = await _context.Restablecimiento.FindAsync(id);
            _context.Restablecimiento.Remove(restablecimiento);
            await _context.SaveChangesAsync();
        }
    }
}
