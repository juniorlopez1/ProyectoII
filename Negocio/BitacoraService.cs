using Datos;
using Entidad;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public interface IBitacoraService
    {
        List<Bitacora> Get();
        Bitacora Create(Bitacora bitacora);

        //Bitacora Get(string Id);
        //void Update(string Id, Bitacora bitacora);
        //void Remove(Bitacora bitacora);
        //void Remove(string Id);
    }

    public class BitacoraService : IBitacoraService
    {
        #region Property - Constructor

        private readonly IMongoCollection<Bitacora> _bitacora;

        public BitacoraService(IMongodbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _bitacora = database.GetCollection<Bitacora>(settings.CollectionName);
        }

        #endregion

        #region CRUD

        public List<Bitacora> Get() =>
            _bitacora.Find(bitacora => true).ToList();

        public Bitacora Create(Bitacora bitacora)
        {
            _bitacora.InsertOne(bitacora);
            return bitacora;
        }


        #endregion

        #region CRUD Unused

        //public Bitacora Get(string id) =>
        //    _bitacora.Find<Bitacora>(bitacora => bitacora.Id == id).FirstOrDefault();

        //public void Update(string id, Bitacora bitacora) =>
        //    _bitacora.ReplaceOne(bitacora => bitacora.Id == id, bitacora);

        //public void Remove(Bitacora bitacora) =>
        //    _bitacora.DeleteOne(bitacora => bitacora.Id == bitacora.Id);

        //public void Remove(string id) =>
        //    _bitacora.DeleteOne(bitacora => bitacora.Id == id);

        #endregion
    }
}
