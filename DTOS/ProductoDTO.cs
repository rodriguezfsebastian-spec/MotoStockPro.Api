namespace MotoStockPro.Api.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string CodigoSku { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int StockMinimo { get; set; }
    }
}