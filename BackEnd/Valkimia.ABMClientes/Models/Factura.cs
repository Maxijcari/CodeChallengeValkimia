using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Valkimia.ABMClientes.Models
{
    public class Factura
    {
        [Key]
        public Guid Id { get; set; }
        
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }

        
        public Cliente? Cliente { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(200)]
        public string Detalle { get; set; }

        [Required]
        public decimal Importe { get; set; }
    }
}
