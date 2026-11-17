using Microsoft.AspNetCore.Mvc;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Services;

namespace MotoStockPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public MovimientosController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpPost]
        public async Task<ActionResult> PostMovimiento(MovimientoRequestDTO dto)
        {
            var exito = await _inventarioService.RegistrarMovimientoAsync(dto);
            if (!exito) return BadRequest(new { mensaje = "No se pudo registrar el movimiento. Verifique el stock o el tipo de movimiento." });
            return Ok(new { mensaje = "Movimiento registrado y stock actualizado exitosamente." });
        }
    }
}