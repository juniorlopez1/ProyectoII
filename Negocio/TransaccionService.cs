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
    public class TransaccionService
    {
        private readonly Context _context;

        public TransaccionService(Context context)
        {
            _context = context;
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transaccion.Any(e => e.Id == id);
        }


        /* CRUD -------------------------------------------------------------------------------------- */

        public async Task<List<Transaccion>> GetTransaccionsAsync()
        {
            var sqlContext = _context.Transaccion
                .Include(t => t.ClienteDestinoNavigation)
                .Include(t => t.ClienteOrigenNavigation)
                .Include(t => t.MetodoPago);
            return await sqlContext.ToListAsync();
        }


        public async Task<List<Transaccion>> GetTransaccionsByIdAsync(int id)
        {
            var sqlContext = _context.Transaccion
                .Include(t => t.ClienteDestinoNavigation)
                .Include(t => t.ClienteOrigenNavigation)
                .Include(t => t.MetodoPago)
                .Where(x => x.ClienteDestino == id || x.ClienteOrigen == id);
            return await sqlContext.ToListAsync();
        }

        public async Task<Transaccion> DetailsTransaccionsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var transaccion = await _context.Transaccion
                .Include(t => t.ClienteDestinoNavigation)
                .Include(t => t.ClienteOrigenNavigation)
                .Include(t => t.MetodoPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return null;
            }
            return transaccion;
        }

        public async Task<Transaccion> CreateTransaccionsAsync(Transaccion transaccion)
        {
            _context.Add(transaccion);
            await _context.SaveChangesAsync();
            return transaccion;
        }

        public async Task<Transaccion> EditTransaccionsAsync(int id, Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return null;
            }
            try
            {
                _context.Update(transaccion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionExists(transaccion.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return transaccion;
        }

        public async Task<Transaccion> DeleteTransaccionAsync(int id)
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            _context.Transaccion.Remove(transaccion);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}
