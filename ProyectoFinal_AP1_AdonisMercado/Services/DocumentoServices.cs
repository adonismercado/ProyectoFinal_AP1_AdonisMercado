using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class DocumentoServices
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly CloudflareR2Service _r2Service;

    public DocumentoServices(IDbContextFactory<Contexto> dbFactory, CloudflareR2Service r2Service)
    {
        _dbFactory = dbFactory;
        _r2Service = r2Service;
    }

    public async Task<bool> Guardar(IBrowserFile archivo, int pedidoId, string tipoDocumento)
    {
        if (archivo == null)
        {
            return false;
        }

        const long maxSize = 10485760; // 10 MB
        if (archivo.Size > maxSize)
        {
            return false;
        }

        var extensiones = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(archivo.Name).ToLowerInvariant();

        if (!extensiones.Contains(extension))
        {
            return false;
        }

        var nombrePersonalizado = Guid.NewGuid().ToString();
        var urlArchivo = await _r2Service.SubirArchivo(archivo, "documentos_pedido", nombrePersonalizado);

        if (string.IsNullOrEmpty(urlArchivo))
        {
            return false;
        }

        var documento = new Documento
        {
            TipoDocumento = tipoDocumento,
            FechaEmision = DateTime.Now,
            NombreOriginal = archivo.Name,
            NombreAlmacenado = nombrePersonalizado + extension,
            RutaDocumento = urlArchivo,
            PedidoId = pedidoId
        };

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Documentos.Add(documento);
        await contexto.SaveChangesAsync();

        var totalDocs = await contexto.Documentos
            .Where(d => d.PedidoId == pedidoId)
            .CountAsync();
        
        if (totalDocs >= 2)
        {
            var pedido = await contexto.Pedidos.FindAsync(pedidoId);
            if (pedido != null)
            {
                pedido.Estado = "Completado";
                await contexto.SaveChangesAsync();
            }
        }

        return true;
    }

    public async Task<List<Documento>> ObtenerDocumentosPorPedido(int pedidoId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .Where(d => d.PedidoId == pedidoId)
            .OrderByDescending(d => d.FechaEmision)
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int documentoId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var documento = await contexto.Documentos.FindAsync(documentoId);

        if (documento == null)
        {
            return false;
        }

        await _r2Service.EliminarArchivo(documento.RutaDocumento);

        contexto.Documentos.Remove(documento);
        return await contexto.SaveChangesAsync() > 0;
    }
}