using Microsoft.AspNetCore.Mvc;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Services;

namespace MotoStockPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenesController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public AlmacenesController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlmacenDTO>>> GetAlmacenes()
        {
            var lista = await _inventarioService.ObtenerAlmacenesAsync();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<AlmacenDTO>> PostAlmacen(AlmacenDTO dto)
        {
            var creado = await _inventarioService.CrearAlmacenAsync(dto);
            return CreatedAtAction(nameof(GetAlmacenes), new { id = creado.Id }, creado);
        }
    }
}