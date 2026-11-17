using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStockPro.Api.Models
{
    [Table("movimientos")]
    public class Movimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tipo { get; set; } = string.Empty; // "ENTRADA" o "SALIDA"

        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required]
        public int AlmacenId { get; set; }
        [ForeignKey("AlmacenId")]
        public Almacen? Almacen { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaMovimiento { get; set; } = DateTime.Now;

        [MaxLength(250)]
        public string Motivo { get; set; } = string.Empty;
    }
}