using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valkimia.ABMClientes.Models;

namespace Valkimia.ABMClientes.Requests
{
    public class ClienteRequest
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Habilitado { get; set; }
        public Guid CiudadId { get; set; }
    }
}
