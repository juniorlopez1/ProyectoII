using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Servicios
{
    public interface IClienteService
    {
        Task<List<ClienteViewModel>> GetAllAsync();
        Task<ClienteViewModel> GetByNombreUsuarioAsync(string nombreUsuario);
        Task<ClienteViewModel> GetByIdAsync(int id);
        Task EditAsync(ClienteViewModel cliente);
        Task CreateAsync(ClienteViewModel cliente);
        Task DeleteAsync(int id);
        Task<ClienteViewModel> AutenticarAsync(CredencialesViewModel credenciales);
    }

    public class ClienteService : ServicioBase, IClienteService
    {
        public ClienteService(string baseUrl) : base(baseUrl)
        {
        }

        /* LOGIN --------------------------------------------------------------------------------------- */

        public async Task<ClienteViewModel> GetByNombreUsuarioAsync(string nombreUsuario)
        {
            return await GetAsync<ClienteViewModel>($"cliente/nombre-usuario/{nombreUsuario}").ConfigureAwait(false);
        }
        public async Task<ClienteViewModel> AutenticarAsync(CredencialesViewModel credenciales)
        {
            return await PostAsync<CredencialesViewModel, ClienteViewModel>("cliente/autenticar", credenciales);
        }


        /* CRUD --------------------------------------------------------------------------------------- */


        public async Task<ClienteViewModel> GetByIdAsync(int id)
        {
            return await GetAsync<ClienteViewModel>($"cliente/{id}").ConfigureAwait(false);
        }
        public async Task<List<ClienteViewModel>> GetAllAsync()
        {
            return await GetAsync<List<ClienteViewModel>>("cliente");
        }
        public async Task CreateAsync(ClienteViewModel cliente)
        {
            await PostAsync("cliente", cliente).ConfigureAwait(false);
        }
        public async Task EditAsync(ClienteViewModel cliente)
        {
            await PutAsync($"cliente/{cliente.Id}", cliente).ConfigureAwait(false);
        }
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"cliente/{id}").ConfigureAwait(false);

        }

    }
}
