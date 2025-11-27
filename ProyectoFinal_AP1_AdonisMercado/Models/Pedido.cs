using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_AP1_AdonisMercado.Models;

public class Pedido
{
    [Key]
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "La fecha de pedido es obligatoria.")]
    public DateTime FechaPedido { get; set; }

    [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$", ErrorMessage = "Solo se permiten letras y espacios.")]
    public string NombrePedido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public string Estado { get; set; } = string.Empty;

    [ForeignKey("Distribuidor")]
    public int DistribuidorId { get; set; }
    [InverseProperty("Pedidos")]
    public Distribuidor Distribuidor { get; set; }

    [InverseProperty("Pedido")]
    public ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    [InverseProperty("Pedido")]
    public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
