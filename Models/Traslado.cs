using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStockPro.Api.Models
{
    [Table("traslados")]
    public class Traslado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AlmacenOrigenId { get; set; }
        [ForeignKey("AlmacenOrigenId")]
        public Almacen? AlmacenOrigen { get; set; }

        [Required]
        public int AlmacenDestinoId { get; set; }
        [ForeignKey("AlmacenDestinoId")]
        public Almacen? AlmacenDestino { get; set; }

        [Required]
        public DateTime FechaTraslado { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string Estado { get; set; } = "PENDIENTE"; // PENDIENTE, RECIBIDO, CANCELADO

        // Relación 1:N con detalles
        public ICollection<DetalleTraslado> Detalles { get; set; } = new List<DetalleTraslado>();
    }

    [Table("detalle_traslados")]
    public class DetalleTraslado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TrasladoId { get; set; }
        [ForeignKey("TrasladoId")]
        public Traslado? Traslado { get; set; }

        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}