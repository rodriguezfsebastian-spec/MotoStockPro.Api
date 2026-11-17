using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStockPro.Api.Models
{
    [Table("ajustes")]
    public class Ajuste
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required]
        public int AlmacenId { get; set; }
        [ForeignKey("AlmacenId")]
        public Almacen? Almacen { get; set; }

        [Required]
        public int CantidadAnterior { get; set; }

        [Required]
        public int CantidadNueva { get; set; }

        [Required]
        [MaxLength(250)]
        public string Justificacion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaAjuste { get; set; } = DateTime.Now;
    }
}