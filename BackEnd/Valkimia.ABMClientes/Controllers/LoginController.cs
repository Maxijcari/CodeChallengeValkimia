using Microsoft.AspNetCore.Mvc;
using Valkimia.ABMClientes.Data;
using Valkimia.ABMClientes.Models;
using Valkimia.ABMClientes.Requests;
using System.Net.Http;
using Valkimia.ABMClientes.Helpers;

namespace Valkimia.ABMClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Helper helper=new Helper();

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest)
        {
            string hashedPasswordFromDatabase;

            try
            {
                    Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Email.Equals(loginRequest.Email));

                    hashedPasswordFromDatabase = cliente.Password;


                    bool passwordMatches = helper.VerifyPassword(loginRequest.Password, hashedPasswordFromDatabase);

                    if (passwordMatches)
                        return Ok();
                    else 
                        return BadRequest();

            }
            catch (Exception ex)
            {

                return Json(ex.ToString());
            }
            
        }

    }

}
