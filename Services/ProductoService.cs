using Microsoft.EntityFrameworkCore;
using MotoStockPro.Api.Data;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Models;

namespace MotoStockPro.Api.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> ObtenerTodosAsync();
        Task<ProductoDTO> CrearAsync(ProductoDTO dto);
    }

    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDTO>> ObtenerTodosAsync()
        {
            var productos = await _context.Productos.ToListAsync();
            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                CodigoSku = p.CodigoSku,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                PrecioUnitario = p.PrecioUnitario,
                StockMinimo = p.StockMinimo
            });
        }

        public async Task<ProductoDTO> CrearAsync(ProductoDTO dto)
        {
            var producto = new Producto
            {
                CodigoSku = dto.CodigoSku,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                PrecioUnitario = dto.PrecioUnitario,
                StockMinimo = dto.StockMinimo
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            dto.Id = producto.Id;
            return dto;
        }
    }
}