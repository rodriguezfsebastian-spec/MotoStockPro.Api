namespace MotoStockPro.Api.DTOs
{
    public class ExistenciaDTO
    {
        public int Id { get; set; }
        public int AlmacenId { get; set; }
        public string AlmacenNombre { get; set; } = string.Empty;
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public string ProductoSku { get; set; } = string.Empty;
        public int CantidadStock { get; set; }
    }
}