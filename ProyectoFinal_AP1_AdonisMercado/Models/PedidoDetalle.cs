using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_AP1_AdonisMercado.Models;

public class PedidoDetalle
{
    [Key]
    public int PedidoDetalleId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 1.")]
    public int Cantidad { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey("Pedido")]
    public int PedidoId { get; set; }
    [InverseProperty("PedidoDetalles")]
    public Pedido Pedido { get; set; }

    [ForeignKey("Vehiculo")]
    public int VehiculoId { get; set; }
    [InverseProperty("PedidoDetalles")]
    public Vehiculo Vehiculo { get; set; }
}
