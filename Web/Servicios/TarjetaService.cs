using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Servicios
{
    public interface ITarjetaService
    {
        Task<List<TarjetaViewModel>> GetAllAsync();
        Task<List<TarjetaViewModel>> GetByClienteIdAsync(int id);
        Task<TarjetaViewModel> GetByIdAsync(int id);
        Task EditAsync(TarjetaViewModel tarjeta);
        Task CreateAsync(TarjetaViewModel tarjeta);
        Task DeleteAsync(int id);
    }

    public class TarjetaService : ServicioBase, ITarjetaService

    {
        public TarjetaService(string baseUrl) : base(baseUrl)
        {

        }


        /* CRUD ------------------------------------------------------------------------------------------ */
        public async Task<List<TarjetaViewModel>> GetByClienteIdAsync(int id)
        {
            return await GetAsync<List<TarjetaViewModel>>($"tarjeta/cliente/{id}").ConfigureAwait(false);
        }
        public async Task<TarjetaViewModel> GetByIdAsync(int id)
        {
            return await GetAsync<TarjetaViewModel>($"tarjeta/{id}").ConfigureAwait(false);
        }
        public async Task<List<TarjetaViewModel>> GetAllAsync()
        {
            return await GetAsync<List<TarjetaViewModel>>("tarjeta");
        }
        public async Task CreateAsync(TarjetaViewModel tarjeta)
        {
            await PostAsync("tarjeta", tarjeta).ConfigureAwait(false);
        }
        public async Task EditAsync(TarjetaViewModel tarjeta)
        {
            await PutAsync($"tarjeta/{tarjeta.Id}", tarjeta).ConfigureAwait(false);
        }
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"tarjeta/{id}").ConfigureAwait(false);

        }
    }
}
