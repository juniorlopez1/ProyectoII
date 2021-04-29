using Acceso;
using Entidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarjetaService
    {
        private readonly Context _context;

        public TarjetaService(Context context)
        {
            _context = context;
        }

        private bool TarjetaExists(int id)
        {
            return _context.Tarjeta.Any(e => e.Id == id);
        } //!! fix


        /* CRUD -------------------------------------------------------------------------------------- */

        public async Task<List<Tarjeta>> GetTarjetasAsync()
        {
            return await _context.Tarjeta.ToListAsync();
        }

        public async Task<List<Tarjeta>> GetByClienteIdAsync(int id)
        {
            return await _context.Tarjeta.Where(x => x.ClienteId == id).ToListAsync();
        }

        public async Task<Tarjeta> DetailsTarjetasAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var tarjeta = await _context.Tarjeta
                .Include(t => t.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarjeta == null)
            {
                return null;
            }
            return tarjeta;
        }

        public async Task<Tarjeta> CreateTarjetasAsync(Tarjeta tarjeta)
        {
            try
            {
                _context.Add(tarjeta);
                await _context.SaveChangesAsync();
                return tarjeta;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Tarjeta> EditTarjetaAsync(int id, Tarjeta tarjeta)
        {
            try
            {
                _context.Update(tarjeta);
                await _context.SaveChangesAsync();

                return tarjeta;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Tarjeta> DeleteTarjetasAsync(int id)
        {
            var tarjeta = await _context.Tarjeta.FindAsync(id);
            _context.Tarjeta.Remove(tarjeta);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}
