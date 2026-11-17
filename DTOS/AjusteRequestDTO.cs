namespace MotoStockPro.Api.DTOs
{
    public class AjusteRequestDTO
    {
        public int ProductoId { get; set; }
        public int AlmacenId { get; set; }
        public int CantidadNueva { get; set; }
        public string Justificacion { get; set; } = string.Empty;
    }
}