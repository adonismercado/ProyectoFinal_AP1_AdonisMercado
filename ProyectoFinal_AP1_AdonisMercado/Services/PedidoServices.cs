using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using System.Linq.Expressions;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class PedidoServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .AnyAsync(p => p.PedidoId == pedidoId);
    }

    private async Task<bool> Insertar(Pedido pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var detalle in pedido.PedidoDetalles)
        {
            var vehiculo = await contexto.Vehiculos
                .FindAsync(detalle.VehiculoId);

            if (vehiculo != null)
            {
                vehiculo.StockVehiculo += detalle.Cantidad;
            }
        }
        contexto.Pedidos.Add(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Pedido pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var pedidoAnterior = await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == pedido.PedidoId);

        if (pedidoAnterior == null)
        {
            return false;
        }

        foreach (var detalleAnterior in pedidoAnterior.PedidoDetalles)
        {
            var vehiculo = await contexto.Vehiculos.FindAsync(detalleAnterior.VehiculoId);
            if (vehiculo != null)
            {
                vehiculo.StockVehiculo -= detalleAnterior.Cantidad;
            }
        }

        var detallesAEliminar = contexto.PedidoDetalles
            .Where(d => d.PedidoId == pedido.PedidoId);
        contexto.PedidoDetalles.RemoveRange(detallesAEliminar);

        foreach (var detalleNuevo in pedido.PedidoDetalles)
        {
            var vehiculo = await contexto.Vehiculos.FindAsync(detalleNuevo.VehiculoId);
            if (vehiculo != null)
            {
                vehiculo.StockVehiculo += detalleNuevo.Cantidad;
            }
        }
        contexto.Pedidos.Update(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Pedido pedido)
    {
        if (!await Existe(pedido.PedidoId))
        {
            return await Insertar(pedido);
        }
        else
        {
            return await Modificar(pedido);
        }
    }

    public async Task<Pedido?> Buscar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .Include(p => p.Documentos)
            .Include(p => p.Distribuidor)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
    }

    public async Task<List<Pedido>> ListarPedidos(Expression<Func<Pedido, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .Include(p => p.Documentos)
            .Include(p => p.Distribuidor)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Deshabilitar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var pedido = await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .Include(p => p.Documentos)
            .Include(p => p.Distribuidor)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        if (pedido == null)
        {
            return false;
        }

        foreach (var detalle in pedido.PedidoDetalles)
        {
            var vehiculo = await contexto.Vehiculos.FindAsync(detalle.VehiculoId);
            if (vehiculo != null)
            {
                vehiculo.StockVehiculo -= detalle.Cantidad;

                if (vehiculo.StockVehiculo < 0)
                {
                    vehiculo.StockVehiculo = 0;
                }
            }
        }

        pedido.isActive = false;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Habilitar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var pedido = await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .Include(p => p.Documentos)
            .Include(p => p.Distribuidor)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        if (pedido == null)
        {
            return false;
        }

        foreach (var detalle in pedido.PedidoDetalles)
        {
            var vehiculo = await contexto.Vehiculos.FindAsync(detalle.VehiculoId);
            if (vehiculo != null)
            {
                vehiculo.StockVehiculo += detalle.Cantidad;
            }
        }
        pedido.isActive = true;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var pedido = await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .Include(p => p.Documentos)
            .Include(p => p.Distribuidor)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        if (pedido == null)
        {
            return false;
        }

        foreach (var detalle in pedido.PedidoDetalles)
        {
            var vehiculo = await contexto.Vehiculos.FindAsync(detalle.VehiculoId);
            if (vehiculo != null)
            {
                vehiculo.StockVehiculo -= detalle.Cantidad;

                if (vehiculo.StockVehiculo < 0)
                {
                    vehiculo.StockVehiculo = 0;
                }
            }
        }

        contexto.PedidoDetalles.RemoveRange(pedido.PedidoDetalles);
        contexto.Pedidos.Remove(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> EnviarPedido(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var pedido = await contexto.Pedidos
            .Include(p => p.PedidoDetalles)
            .Include(p => p.Documentos)
            .Include(p => p.Distribuidor)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        if (pedido == null)
        {
            return false; 
        }

        pedido.Estado = "Enviado";

        contexto.Pedidos.Update(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }
}
