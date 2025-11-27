using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_AP1_AdonisMercado.Models;

public class Documento
{
    [Key]
    public int DocumentoId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }

    [ForeignKey("Pedido")]
    public int PedidoId { get; set; }
    [InverseProperty("Documentos")]
    public Pedido Pedido { get; set; }
}
