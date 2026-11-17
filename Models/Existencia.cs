using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStockPro.Api.Models
{
    [Table("existencias")]
    public class Existencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AlmacenId { get; set; }
        [ForeignKey("AlmacenId")]
        public Almacen? Almacen { get; set; }

        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required]
        public int CantidadStock { get; set; }
    }
}