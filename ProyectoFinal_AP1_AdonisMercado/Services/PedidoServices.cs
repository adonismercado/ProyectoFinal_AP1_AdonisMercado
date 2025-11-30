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
        contexto.Pedidos.Add(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Pedido pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
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

        pedido.isActive = false;
        return await contexto.SaveChangesAsync() > 0;
    }
}
