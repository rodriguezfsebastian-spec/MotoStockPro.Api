namespace MotoStockPro.Api.DTOs
{
    public class TrasladoRequestDTO
    {
        public int AlmacenOrigenId { get; set; }
        public int AlmacenDestinoId { get; set; }
        public List<DetalleTrasladoDTO> Detalles { get; set; } = new List<DetalleTrasladoDTO>();
    }

    public class DetalleTrasladoDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}