using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Valkimia.ABMClientes.Data;
using Valkimia.ABMClientes.Models;

namespace Valkimia.ABMClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FacturaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateFactura([FromBody] Factura factura)
        {
            try
            {
                Factura MyFactura = new Factura
                {
                    Id = Guid.NewGuid(),
                    ClienteId = factura.ClienteId,
                    Fecha = factura.Fecha,
                    Detalle = factura.Detalle,
                    Importe = factura.Importe,
                };
                _context.Add(MyFactura);
                _context.SaveChanges();
                return Ok(MyFactura);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult GetAllFacturas()
        {
            try
            {
                List<Factura> Facturas = _context.Facturas.ToList();
                string jsonFacturas = JsonConvert.SerializeObject(Facturas);
                if (jsonFacturas != null)
                {
                    return Json(jsonFacturas);
                }
                else
                {
                    return NotFound("No se encontró ninguna factura");
                }

            }
            catch (Exception ex) 
            {
                return Json(ex.ToString());
            }
        }
        [HttpPut]
        public IActionResult ModifyFactura()
        {
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteFactura()
        {
            return View();
        }
    }
}
