using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Valkimia.ABMClientes.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string Domicilio { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Habilitado { get; set; }
        
        [ForeignKey("Ciudad")]
        public Guid CiudadId { get; set; }
        
        public Ciudad? Ciudad { get; set; }

        public ICollection<Factura>? Facturas { get; set; }

    }
}
