using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using System.Linq.Expressions;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class DocumentoServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .AnyAsync(d => d.DocumentoId == documentoId);
    }

    private async Task<bool> Insertar(Documento documento)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Documentos.Add(documento);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Documento documento)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Documentos.Update(documento);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Documento documento)
    {
        if (!await Existe(documento.DocumentoId))
        {
            return await Insertar(documento);
        }
        else
        {
            return await Modificar(documento);
        }
    }

    public async Task<Documento?> Buscar(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .Include(d => d.Pedido)
            .FirstOrDefaultAsync(d => d.DocumentoId == documentoId);
    }

    public async Task<List<Documento>> Listar(Expression<Func<Documento, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Documentos
            .Include(d => d.Pedido)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int documentoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var documento = await contexto.Documentos
            .FirstOrDefaultAsync(d => d.DocumentoId == documentoId);

        if (documento == null)
        {
            return false;
        }

        contexto.Documentos.Remove(documento);
        return await contexto.SaveChangesAsync() > 0;
    }
}
