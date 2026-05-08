namespace ApiDescuentosCobranza.CQRS.Commands
{
    // Clase usada para registrar facturas
    public class CreateFacturaCommand
    {
        public int ClienteId { get; set; }

        public decimal Valor { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public bool Aplicada { get; set; }
    }
}
