using Microsoft.AspNetCore.Mvc;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Services;

namespace MotoStockPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrasladosController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public TrasladosController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpPost]
        public async Task<ActionResult> PostTraslado(TrasladoRequestDTO dto)
        {
            await _inventarioService.RegistrarTrasladoAsync(dto);
            return Ok(new { mensaje = "Traslado solicitado exitosamente." });
        }

        [HttpPut("{id}/recibir")]
        public async Task<IActionResult> RecibirTraslado(int id)
        {
            var exito = await _inventarioService.RecibirTrasladoAsync(id);
            if (!exito) return BadRequest(new { mensaje = "No se pudo recibir el traslado (verifique stock en origen o si ya fue recibido)." });
            return Ok(new { mensaje = "Traslado recibido y stock actualizado exitosamente." });
        }
    }
}