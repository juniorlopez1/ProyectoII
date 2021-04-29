using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Servicios
{
    public interface ITransaccionService
    {
        Task<List<TransaccionViewModel>> GetAllAsync();
        Task<List<TransaccionViewModel>> GetAllByCustomerIdAsync(int id);
        Task<TransaccionViewModel> GetByIdAsync(int id);
        Task EditAsync(TransaccionViewModel transaccion);
        Task CreateAsync(TransaccionViewModel transaccion);

        Task DeleteAsync(int id);
    }
    public class TransaccionService : ServicioBase, ITransaccionService
    {
        public TransaccionService(string baseUrl) : base(baseUrl)
        {

        }



        /* CRUD ----------------------------------------------------------------------------------------- */
        public async Task<List<TransaccionViewModel>> GetAllByCustomerIdAsync(int id)
        {
            return await GetAsync<List<TransaccionViewModel>>($"transaccion/customer/{id}").ConfigureAwait(false);
        }
        public async Task<TransaccionViewModel> GetByIdAsync(int id)
        {
            return await GetAsync<TransaccionViewModel>($"transaccion/{id}").ConfigureAwait(false);
        }
        public async Task<List<TransaccionViewModel>> GetAllAsync()
        {
            return await GetAsync<List<TransaccionViewModel>>("transaccion");
        }
        public async Task CreateAsync(TransaccionViewModel transaccion)
        {
            await PostAsync("transaccion", transaccion).ConfigureAwait(false);
        }
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"transaccion/{id}").ConfigureAwait(false);
        }
        public async Task EditAsync(TransaccionViewModel transaccion)
        {
            await PutAsync($"transaccion/{transaccion.Id}", transaccion).ConfigureAwait(false);
        }

    }
}
