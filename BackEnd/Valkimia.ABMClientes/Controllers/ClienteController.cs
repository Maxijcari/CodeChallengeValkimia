using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Valkimia.ABMClientes.Data;
using Valkimia.ABMClientes.Models;

namespace Valkimia.ABMClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] Cliente cliente)
        {
            try
            {
                Cliente MyCliente = new Cliente
                {
                    Id = Guid.NewGuid(),
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,    
                    Domicilio = cliente.Domicilio,
                    Email = cliente.Email,
                    Password = cliente.Password,
                    Habilitado = cliente.Habilitado,
                    CiudadId = cliente.CiudadId,
                };
    
                _context.Add(MyCliente);
                _context.SaveChanges();
                return Ok(MyCliente);
            }
            catch (Exception ex)
            {

                return Json(ex.ToString());

            }
            
        }
        [HttpGet]
        public IActionResult GetAllClients()
        {
            try 
            {
                List<Cliente> Clientes = _context.Clientes.ToList();
                string jsonClientes = JsonConvert.SerializeObject(Clientes);
                if (jsonClientes != null)
                {
                    return Json(jsonClientes);
                }
                else
                {
                    return NotFound("No se encontró ningun cliente");
                }
            }catch(Exception ex) 
            {
                return Json(ex.ToString());
            }
        }

        [HttpPut]
        public IActionResult ModifyClient()
        {
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteClient() 
        {
            return View(); 
        }
    }
}
