using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Valkimia.ABMClientes.Data;
using Valkimia.ABMClientes.Requests;
using Valkimia.ABMClientes.Helpers;
using Valkimia.ABMClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace Valkimia.ABMClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Helper helper = new Helper();
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClienteRequest cliente)
        {
            try
            {
                Cliente MyCliente = new Cliente() { 
                    Id = Guid.NewGuid(),
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,    
                    Domicilio = cliente.Domicilio,
                    Email = cliente.Email,
                    Password = helper.HashPassword(cliente.Password),
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

        [HttpPut("{Id}")]
        public IActionResult ModifyClient(Guid Id,Cliente cliente)
        {
            try
            {
                bool ExisteCliente = _context.Clientes.Any(c => c.Id == Id);
                if (ExisteCliente)
                {
                    Cliente ClienteExistente = _context.Clientes.Find(Id);
                    if (ClienteExistente != null)
                    {
                        ClienteExistente.Nombre = cliente.Nombre;
                        ClienteExistente.Apellido = cliente.Apellido;
                        ClienteExistente.Email = cliente.Email;
                        ClienteExistente.Password = helper.HashPassword(cliente.Password);
                        ClienteExistente.Domicilio = cliente.Domicilio;
                        ClienteExistente.CiudadId = cliente.CiudadId;
                        ClienteExistente.Habilitado = cliente.Habilitado;
                        _context.Entry(ClienteExistente).State = EntityState.Modified;
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                return Json(ex.ToString()); 
            }
            
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteClient(Guid Id) 
        {
            try
            {
                Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == Id);
                if (cliente == null)
                {
                    _context.Clientes.Remove(cliente);
                    _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}
