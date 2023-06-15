using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpPut("{Id}")]
        public IActionResult ModifyFactura(Guid Id,Factura factura)
        {
            try
            {
                bool ExisteFactura = _context.Facturas.Any(c => c.Id == Id);
                if (ExisteFactura)
                {
                    Factura FacturaExistente = _context.Facturas.Find(Id);
                    if (FacturaExistente != null)
                    {
                        FacturaExistente.Detalle = factura.Detalle;
                        FacturaExistente.Importe = factura.Importe;
                        FacturaExistente.ClienteId = factura.ClienteId;
                        FacturaExistente.Fecha = factura.Fecha;
                        _context.Entry(FacturaExistente).State = EntityState.Modified;
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
        public IActionResult DeleteFactura(Guid Id)
        {
            try
            {
                Factura FacturaExistente = _context.Facturas.FirstOrDefault(c => c.Id == Id);
                if (FacturaExistente != null)
                {
                    _context.Facturas.Remove(FacturaExistente);
                    _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}
