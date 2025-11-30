using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_AP1_AdonisMercado.Models;

public class Distribuidor
{
    [Key]
    public int DistribuidorId { get; set; }

    [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$", ErrorMessage = "Solo se permiten letras y espacios.")]
    public string Nombre { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; } = string.Empty;

    [RegularExpression(@"^[0-9\- ]+$", ErrorMessage = "El teléfono solo permite números, espacios y guiones.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El Fax es obligatorio.")]
    public string Fax { get; set; } = string.Empty;

    public bool isActive { get; set; } = true;

    [InverseProperty("Distribuidor")]
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
