using Microsoft.EntityFrameworkCore;
using MotoStockPro.Api.Data;
using MotoStockPro.Api.DTOs;
using MotoStockPro.Api.Models;

namespace MotoStockPro.Api.Services
{
    public interface IInventarioService
    {
        Task<IEnumerable<AlmacenDTO>> ObtenerAlmacenesAsync();
        Task<AlmacenDTO> CrearAlmacenAsync(AlmacenDTO dto);
        Task<IEnumerable<ExistenciaDTO>> ObtenerExistenciasAsync();
        Task<IEnumerable<ExistenciaDTO>> ObtenerExistenciasPorAlmacenAsync(int almacenId);
        Task<bool> RegistrarMovimientoAsync(MovimientoRequestDTO dto);
        Task<bool> RegistrarTrasladoAsync(TrasladoRequestDTO dto);
        Task<bool> RecibirTrasladoAsync(int trasladoId);
        Task<bool> RegistrarAjusteAsync(AjusteRequestDTO dto);
    }

    public class InventarioService : IInventarioService
    {
        private readonly AppDbContext _context;

        public InventarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlmacenDTO>> ObtenerAlmacenesAsync()
        {
            var lista = await _context.Almacenes.ToListAsync();
            return lista.Select(a => new AlmacenDTO { Id = a.Id, Nombre = a.Nombre, UbicacionFisica = a.UbicacionFisica });
        }

        public async Task<AlmacenDTO> CrearAlmacenAsync(AlmacenDTO dto)
        {
            var almacen = new Almacen { Nombre = dto.Nombre, UbicacionFisica = dto.UbicacionFisica };
            _context.Almacenes.Add(almacen);
            await _context.SaveChangesAsync();
            dto.Id = almacen.Id;
            return dto;
        }

        public async Task<IEnumerable<ExistenciaDTO>> ObtenerExistenciasAsync()
        {
            var existencias = await _context.Existencias
                .Include(e => e.Almacen)
                .Include(e => e.Producto)
                .ToListAsync();

            return existencias.Select(e => new ExistenciaDTO
            {
                Id = e.Id,
                AlmacenId = e.AlmacenId,
                AlmacenNombre = e.Almacen?.Nombre ?? string.Empty,
                ProductoId = e.ProductoId,
                ProductoNombre = e.Producto?.Nombre ?? string.Empty,
                ProductoSku = e.Producto?.CodigoSku ?? string.Empty,
                CantidadStock = e.CantidadStock
            });
        }

        public async Task<IEnumerable<ExistenciaDTO>> ObtenerExistenciasPorAlmacenAsync(int almacenId)
        {
            var existencias = await _context.Existencias
                .Where(e => e.AlmacenId == almacenId)
                .Include(e => e.Almacen)
                .Include(e => e.Producto)
                .ToListAsync();

            return existencias.Select(e => new ExistenciaDTO
            {
                Id = e.Id,
                AlmacenId = e.AlmacenId,
                AlmacenNombre = e.Almacen?.Nombre ?? string.Empty,
                ProductoId = e.ProductoId,
                ProductoNombre = e.Producto?.Nombre ?? string.Empty,
                ProductoSku = e.Producto?.CodigoSku ?? string.Empty,
                CantidadStock = e.CantidadStock
            });
        }

        public async Task<bool> RegistrarMovimientoAsync(MovimientoRequestDTO dto)
        {
            var existencia = await _context.Existencias
                .FirstOrDefaultAsync(e => e.AlmacenId == dto.AlmacenId && e.ProductoId == dto.ProductoId);

            if (dto.Tipo.ToUpper() == "ENTRADA")
            {
                if (existencia == null)
                {
                    existencia = new Existencia { AlmacenId = dto.AlmacenId, ProductoId = dto.ProductoId, CantidadStock = dto.Cantidad };
                    _context.Existencias.Add(existencia);
                }
                else
                {
                    existencia.CantidadStock += dto.Cantidad;
                }
            }
            else if (dto.Tipo.ToUpper() == "SALIDA")
            {
                if (existencia == null || existencia.CantidadStock < dto.Cantidad) return false;
                existencia.CantidadStock -= dto.Cantidad;
            }
            else
            {
                return false;
            }

            var movimiento = new Movimiento
            {
                Tipo = dto.Tipo.ToUpper(),
                ProductoId = dto.ProductoId,
                AlmacenId = dto.AlmacenId,
                Cantidad = dto.Cantidad,
                Motivo = dto.Motivo,
                FechaMovimiento = DateTime.Now
            };

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegistrarTrasladoAsync(TrasladoRequestDTO dto)
        {
            var traslado = new Traslado
            {
                AlmacenOrigenId = dto.AlmacenOrigenId,
                AlmacenDestinoId = dto.AlmacenDestinoId,
                Estado = "PENDIENTE",
                FechaTraslado = DateTime.Now,
                Detalles = dto.Detalles.Select(d => new DetalleTraslado
                {
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad
                }).ToList()
            };

            _context.Traslados.Add(traslado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RecibirTrasladoAsync(int trasladoId)
        {
            var traslado = await _context.Traslados
                .Include(t => t.Detalles)
                .FirstOrDefaultAsync(t => t.Id == trasladoId);

            if (traslado == null || traslado.Estado == "RECIBIDO") return false;

            foreach (var detalle in traslado.Detalles)
            {
                var stockOrigen = await _context.Existencias
                    .FirstOrDefaultAsync(e => e.AlmacenId == traslado.AlmacenOrigenId && e.ProductoId == detalle.ProductoId);
                
                if (stockOrigen == null || stockOrigen.CantidadStock < detalle.Cantidad) return false;
                stockOrigen.CantidadStock -= detalle.Cantidad;

                var stockDestino = await _context.Existencias
                    .FirstOrDefaultAsync(e => e.AlmacenId == traslado.AlmacenDestinoId && e.ProductoId == detalle.ProductoId);

                if (stockDestino == null)
                {
                    stockDestino = new Existencia { AlmacenId = traslado.AlmacenDestinoId, ProductoId = detalle.ProductoId, CantidadStock = detalle.Cantidad };
                    _context.Existencias.Add(stockDestino);
                }
                else
                {
                    stockDestino.CantidadStock += detalle.Cantidad;
                }
            }

            traslado.Estado = "RECIBIDO";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegistrarAjusteAsync(AjusteRequestDTO dto)
        {
            var existencia = await _context.Existencias
                .FirstOrDefaultAsync(e => e.AlmacenId == dto.AlmacenId && e.ProductoId == dto.ProductoId);

            if (existencia == null) return false;

            var ajuste = new Ajuste
            {
                ProductoId = dto.ProductoId,
                AlmacenId = dto.AlmacenId,
                CantidadAnterior = existencia.CantidadStock,
                CantidadNueva = dto.CantidadNueva,
                Justificacion = dto.Justificacion,
                FechaAjuste = DateTime.Now
            };

            existencia.CantidadStock = dto.CantidadNueva;
            _context.Ajustes.Add(ajuste);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}