using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using System.Linq.Expressions;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class VehiculoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .AnyAsync(v => v.VehiculoId == vehiculoId);
    }

    public async Task<int> InsertarConId(Vehiculo vehiculo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Vehiculos.Add(vehiculo);
        await contexto.SaveChangesAsync();

        return vehiculo.VehiculoId;
    }

    public async Task<bool> Modificar(Vehiculo vehiculo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Vehiculos.Update(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> ActualizarImagen(int idVehiculo, string imagenUrl)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var vehiculo = await contexto.Vehiculos.FindAsync(idVehiculo);

        if (vehiculo == null)
        { 
            return false; 
        }

        vehiculo.ImagenUrl = imagenUrl;

        return await contexto.SaveChangesAsync() > 0;
    }


    public async Task<Vehiculo?> Buscar(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .Include(v => v.PedidoDetalles)
            .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);
    }

    public async Task<List<Vehiculo>> ListarVehiculos(Expression<Func<Vehiculo, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .Include(v => v.PedidoDetalles)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Deshabilitar(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos
            .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);

        if (vehiculo == null)
        {
            return false;
        }

        vehiculo.isActive = false;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Habilitar(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos
            .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);

        if (vehiculo == null)
        {
            return false;
        }

        vehiculo.isActive = true;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int vehiculoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos
            .FirstOrDefaultAsync(p => p.VehiculoId == vehiculoId);

        if (vehiculo == null)
        {
            return false;
        }

        contexto.Vehiculos.Remove(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
}
