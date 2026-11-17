using Microsoft.AspNetCore.Mvc;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Services;

namespace MotoStockPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciasController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public ExistenciasController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistenciaDTO>>> GetExistencias()
        {
            return Ok(await _inventarioService.ObtenerExistenciasAsync());
        }

        [HttpGet("almacen/{almacenId}")]
        public async Task<ActionResult<IEnumerable<ExistenciaDTO>>> GetStockPorAlmacen(int almacenId)
        {
            return Ok(await _inventarioService.ObtenerExistenciasPorAlmacenAsync(almacenId));
        }
    }
}