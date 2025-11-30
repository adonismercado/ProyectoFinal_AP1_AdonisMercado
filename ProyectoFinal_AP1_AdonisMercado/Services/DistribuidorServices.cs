using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using System.Linq.Expressions;

namespace ProyectoFinal_AP1_AdonisMercado.Services;
public class DistribuidorServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int distribuidorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Distribuidores
            .AnyAsync(d => d.DistribuidorId == distribuidorId);
    }

    private async Task<bool> Insertar(Distribuidor distribuidor)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Distribuidores.Add(distribuidor);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Distribuidor distribuidor)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Distribuidores.Update(distribuidor);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Distribuidor distribuidor)
    {
        if (!await Existe(distribuidor.DistribuidorId))
        {
            return await Insertar(distribuidor);
        }
        else
        {
            return await Modificar(distribuidor);
        }
    }

    public async Task<Distribuidor?> Buscar(int distribuidorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Distribuidores
            .Include(d => d.Pedidos)
            .FirstOrDefaultAsync(d => d.DistribuidorId == distribuidorId);
    }

    public async Task<List<Distribuidor>> ListarDistribuidores(Expression<Func<Distribuidor, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Distribuidores
            .Include(d => d.Pedidos)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int distribuidorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var distribuidor = await Buscar(distribuidorId);

        if (distribuidor == null)
        {
            return false;
        }

        distribuidor.isActive = false;
        return await contexto.SaveChangesAsync() > 0;
    }
}