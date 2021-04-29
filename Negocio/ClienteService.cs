using Acceso;
using Entidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Negocio
{
    public class ClienteService
    {
        private readonly Context _context;

        public ClienteService(Context context)
        {
            _context = context;
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }

        /* CRUD -------------------------------------------------------------------------------------- */

        public async Task<List<Cliente>> ReadClientesAsync()
        {
            //return await _context.Clientes.FromSqlInterpolated($"EXECUTE dbo.pSelectClienteAll").ToListAsync();
            return await _context.Cliente.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Cliente> DetailsClientesAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            //var cliente = (await _context.Clientes.FromSqlInterpolated($"EXECUTE dbo.pSelectClienteByID {id.Value} ").ToListAsync()).FirstOrDefault();
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return null;
            }
            return cliente;
        }

        public async Task<Cliente> CreateClientesAsync(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> EditClientesAsync(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return null;
            }
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return cliente;
        }

        public async Task<Cliente> DeleteClientesAsync(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }
            cliente.Estado = false;
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }



        /* LOGIN -------------------------------------------------------------------------------------- */

        public async Task<Cliente> GetByNombreDeUsuarioAsync(string nombreUsuario)
        {
            return await _context.Cliente.Where(c => c.NombreUsuario == nombreUsuario)
                .FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetByNombreUsuarioAndContrasenaAsync(string nombreUsuario, string contrasena)
        {
            return await _context.Cliente.Where(c => c.NombreUsuario == nombreUsuario && c.Contrasena == contrasena)
                .FirstOrDefaultAsync();
        }

    }
}
