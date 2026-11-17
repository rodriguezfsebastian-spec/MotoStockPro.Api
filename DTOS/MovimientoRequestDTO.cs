namespace MotoStockPro.Api.DTOs
{
    public class MovimientoRequestDTO
    {
        public string Tipo { get; set; } = string.Empty; // "ENTRADA" o "SALIDA"
        public int ProductoId { get; set; }
        public int AlmacenId { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}