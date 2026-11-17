using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStockPro.Api.Models
{
    [Table("almacenes")]
    public class Almacen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string UbicacionFisica { get; set; } = string.Empty;

        // Relación 1:N con Existencias
        public ICollection<Existencia> Existencias { get; set; } = new List<Existencia>();
    }
}