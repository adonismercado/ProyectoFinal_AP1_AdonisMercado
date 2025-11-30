using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_AP1_AdonisMercado.Models;

public class Documento
{
    [Key]
    public int DocumentoId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
    [Required]
    public string NombreOriginal { get; set; } = string.Empty;
    [Required]
    public string NombreAlmacenado { get; set; } = string.Empty;
    [Required]
    public string RutaDocumento { get; set;} = string.Empty;
    public bool isActive { get; set; } = true;

    [ForeignKey("Pedido")]
    public int PedidoId { get; set; }
    [InverseProperty("Documentos")]
    public Pedido? Pedido { get; set; }
}
