using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using System.Linq.Expressions;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class DocumentoServices(IDbContextFactory<Contexto> dbFactory, IWebHostEnvironment env)
{
    public async Task<List<Documento>> ObtenerDocsPorPedido(int pedidoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .Where(d => d.PedidoId == pedidoId)
            .ToListAsync();
    }

    public async Task GuardarDocumento(int pedidoId, string tipoDocumento, DateTime fechaEmision, IBrowserFile archivo)
    {
        var extension = Path.GetExtension(archivo.Name).ToLower();
        var permitidos = new[] { ".pdf", ".jpg", ".jpeg", ".png" };

        if (!permitidos.Contains(extension))
        {
            throw new Exception("Tipo de archivo no permitido.");
        }

        string nombreUnico = $"{Guid.NewGuid()}{extension}";

        string carpeta = Path.Combine(env.WebRootPath, "documentos_pedidos");

        if (!Directory.Exists(carpeta))
        {
            Directory.CreateDirectory(carpeta);
        }

        string rutaFisica = Path.Combine(carpeta, nombreUnico);

        using var stream = new FileStream(rutaFisica, FileMode.Create);
        await archivo.OpenReadStream(maxAllowedSize: 20_000_000).CopyToAsync(stream);

        var documento = new Documento
        {
            PedidoId = pedidoId,
            TipoDocumento = tipoDocumento,
            FechaEmision = fechaEmision,
            NombreOriginal = archivo.Name,
            NombreAlmacenado = nombreUnico,
            RutaDocumento = $"documentos_pedidos/{nombreUnico}",
            ContentType = archivo.ContentType
        };

        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Documentos.Add(documento);
        await contexto.SaveChangesAsync();
    }

    public async Task<List<Documento>> ListarDocumentos(Expression<Func<Documento, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Documentos
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int documentoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var documento = await contexto.Documentos
            .FirstOrDefaultAsync(d => d.DocumentoId == documentoId);

        if (documento == null)
        {
            return false;
        }
        else
        {
            string ruta = Path.Combine(env.WebRootPath, documento.RutaDocumento);

            if (File.Exists(ruta))
            {
                try
                {
                    File.Delete(ruta);
                }
                catch { }
            }

            contexto.Documentos.Remove(documento);
            return await contexto.SaveChangesAsync() > 0;
        }
    }
}
