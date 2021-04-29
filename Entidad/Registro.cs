using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entidad
{
    public class Registro
    {
        /* ID necesario para index de mongodb  ---------------------------------------------------------------- */
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        /* Propiedades: 2016-05-18T16:00:00Z, sample@mail.com, Cliente, POST, exitoso --------------------------
        Se implementa en mongodb como bitacora registro -------------------------------------------------------- */
        public DateTime FechaRegistro { get; set; }
        public string NombreUsuario { get; set; }
        public string Entidad { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }

    }
}
