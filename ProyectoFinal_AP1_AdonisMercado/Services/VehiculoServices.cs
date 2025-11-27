using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class VehiculoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .AnyAsync(v => v.VehiculoId == vehiculoId);
    }

    private async Task<bool> Insertar(Vehiculo vehiculo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Vehiculos.Add(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Vehiculo vehiculo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Vehiculos.Update(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Vehiculo vehiculo)
    {
        if (!await Existe(vehiculo.VehiculoId))
        {
            return await Insertar(vehiculo);
        }
        else
        {
            return await Modificar(vehiculo);
        }
    }

    public async Task<Vehiculo?> Buscar(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .Include(v => v.PedidoDetalles)
            .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);
    }

    public async Task<List<Vehiculo>> ListarVehiculos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .Include(v => v.PedidoDetalles)
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var vehiculo = await Buscar(vehiculoId);

        if (vehiculo != null)
        {
            contexto.Vehiculos.Remove(vehiculo);
            return await contexto.SaveChangesAsync() > 0;
        }

        return false;
    }
}
