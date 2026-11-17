using Microsoft.AspNetCore.Mvc;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Services;

namespace MotoStockPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AjustesController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public AjustesController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpPost]
        public async Task<ActionResult> PostAjuste(AjusteRequestDTO dto)
        {
            var exito = await _inventarioService.RegistrarAjusteAsync(dto);
            if (!exito) return BadRequest(new { mensaje = "No se encontró registro de inventario para este producto en el almacén indicado." });
            return Ok(new { mensaje = "Ajuste de inventario registrado correctamente." });
        }
    }
}