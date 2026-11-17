using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStockPro.Api.Models
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CodigoSku { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public int StockMinimo { get; set; } // Para alertas de mínimos (RF-07-09)

        // Relaciones
        public ICollection<Existencia> Existencias { get; set; } = new List<Existencia>();
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}