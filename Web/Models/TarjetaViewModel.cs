using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TarjetaViewModel
    {
        public int Id { get; set; }
        [Range(5, long.MaxValue, ErrorMessage = "El numero de tarjeta no es valido")]
        public long NumeroTarjeta { get; set; }
        public int ClienteId { get; set; }
        public DateTime Expiracion { get; set; }
        public string Titular { get; set; }
        public short Csv { get; set; }
        public string TipoTarjeta { get; set; }


        #region Mask

        //public string MaskNumeroTarjeta
        //{
        //    get
        //    {
        //        return NumeroTarjeta.ToString().Remove(0, 11).Insert(0, "****-****-****-".ToString());
        //    }
        //}

        //public string MaskCSV
        //{
        //    get
        //    {
        //        if (Csv.ToString().Length == 1)
        //        {
        //            return string.Concat("00", Csv);
        //        }
        //        else if (Csv.ToString().Length == 2)
        //        {
        //            return string.Concat("0", Csv);
        //        }
        //        else
        //        {
        //            return Csv.ToString();
        //        }
        //    }
        //}

        //public string MaskExpiration
        //{
        //    get
        //    {
        //        return Expiracion.ToString("MMMM yy");
        //    }
        //} 

        #endregion

        //Otras entidades
        public ClienteViewModel ClienteNavigation { get; set; }
        //public TransaccionViewModel Transaccions { get; set; }
    }
}
