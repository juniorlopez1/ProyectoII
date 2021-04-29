using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Data;

namespace Web.Services
{
    public interface IBitacoraWebService
    {
        Task<List<BitacoraViewModel>> Get();
        Task Create(BitacoraViewModel registro);
        //Task Update(BitacoraViewModel registro);
        //Task Delete(string ID);
        //Task<BitacoraViewModel> GetBy(string Id);
    }

    public class BitacoraWebService : IBitacoraWebService
    {
        #region Property - Constructor

        private readonly IMongoCollection<BitacoraViewModel> _bitacora;

        public BitacoraWebService(IMongodbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _bitacora = database.GetCollection<BitacoraViewModel>(settings.CollectionName);
        }

        #endregion

        #region CRUD

        public async Task<List<BitacoraViewModel>> Get()
        {
            return await _bitacora.Find(_ => true).ToListAsync();
        }

        public async Task Create(BitacoraViewModel registro)
        {
            await _bitacora.InsertOneAsync(registro);
        }

        #endregion

        #region CRUD Unused

        //public async Task Delete(string ID)
        //{
        //    //FilterDefinition<BitacoraViewModel> data = Builders<BitacoraViewModel>.Filter.Eq("Id", id);
        //    await _bitacora.DeleteOneAsync(ID);
        //}

        //public async Task<BitacoraViewModel> GetBy(string Id)
        //{
        //    return await _bitacora.Find(Id).FirstOrDefaultAsync();
        //}

        //public async Task Update(BitacoraViewModel registro)
        //{
        //    await _bitacora.ReplaceOneAsync(filter: g => g.Id == registro.Id, replacement: registro);
        //} 

        #endregion

    }
}
