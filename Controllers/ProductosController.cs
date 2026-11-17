using Microsoft.AspNetCore.Mvc;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Services;

namespace MotoStockPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> PostProducto(ProductoDTO productoDto)
        {
            var creado = await _productoService.CrearAsync(productoDto);
            return CreatedAtAction(nameof(GetProductos), new { id = creado.Id }, creado);
        }
    }
}