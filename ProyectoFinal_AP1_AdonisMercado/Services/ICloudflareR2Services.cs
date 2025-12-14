using Microsoft.AspNetCore.Components.Forms;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public interface ICloudflareR2Service
{
    Task<string?> SubirArchivo(
        IBrowserFile archivo,
        string carpeta,
        string? nombrePersonalizado = null);

    Task<bool> EliminarArchivo(string urlCompleta);
}
