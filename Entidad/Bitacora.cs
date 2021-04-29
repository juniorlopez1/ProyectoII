using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Bitacora
    {
        /*Utiliza el Bson de mongodb*/

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        /*Necesario para la entidad de mongo */
        //{Fecha, Entidad, Crear, Cliente}
        public string Id { get; set; }
        public DateTime fecha { get; set; }
        public string Entidad { get; set; }
        public string Detalle { get; set; }
        public string Accion { get; set; }
    }
}
