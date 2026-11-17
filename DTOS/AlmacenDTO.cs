namespace MotoStockPro.Api.DTOs
{
    public class AlmacenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UbicacionFisica { get; set; } = string.Empty;
    }
}