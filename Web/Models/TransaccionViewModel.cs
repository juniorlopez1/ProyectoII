namespace Web.Models
{
    public class TransaccionViewModel
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public int ClienteOrigen { get; set; }
        public int ClienteDestino { get; set; }
        public double Monto { get; set; }
        public int MetodoPagoId { get; set; }
        public string Detalle { get; set; }

        //Otras entidades
        public ClienteViewModel ClienteDestinoNavigation { get; set; }
        public ClienteViewModel ClienteOrigenNavigation { get; set; }
        public TarjetaViewModel MetodoPago { get; set; }

    }
}
