namespace ApiDescuentosCobranza.CQRS.Commands
{
    // Clase usada para registrar un nuevo cliente
    public class CreateClienteCommand
    {
        public string Nombre { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public decimal SaldoTotal { get; set; }
    }
}
