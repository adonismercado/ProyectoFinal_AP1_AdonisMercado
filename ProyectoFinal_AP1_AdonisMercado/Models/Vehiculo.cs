using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_AP1_AdonisMercado.Models;

public class Vehiculo
{
    [Key]
    public int VehiculoId { get; set; }

    [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$", ErrorMessage = "Marca incorrecta. Solo se permite usar letras.")]
    public string MarcaVehiculo { get; set; } = string.Empty;

    [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúñÑ0-9 ]+$", ErrorMessage = "Modelo incorrecto. Solo se permite usar letras y números.")]
    public string ModeloVehiculo { get; set; } = string.Empty;

    [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$", ErrorMessage = "Color incorrecto. Solo se permite usar letras.")]
    public string ColorVehiculo { get; set; } = string.Empty;

    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Número de chasis inválido. Solo se permite usar letras y números.")]
    public string NumeroChasis { get; set; } = string.Empty;

    [Required(ErrorMessage = "El año de fabricación es obligatorio.")]
    public int AnioFabricacion { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public string EstadoVehiculo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de combustible es obligatorio.")]
    public string TipoCombustible { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [InverseProperty("Vehiculo")]
    public ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
