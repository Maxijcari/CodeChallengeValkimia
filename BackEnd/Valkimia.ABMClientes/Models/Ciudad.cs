using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Valkimia.ABMClientes.Models
{
    public class Ciudad
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [JsonIgnore]
        public ICollection<Cliente>? Clientes { get; set; }
    }
}
