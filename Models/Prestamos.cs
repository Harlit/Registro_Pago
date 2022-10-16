using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prestamos
{
    [Key]
    [Required(ErrorMessage = "El PrestamoId es requerido")]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "La Fecha es requerida")]
    public DateTimeOffset Fecha { get; set; }

    [Required(ErrorMessage = "La fecha vence es requerida")]
    public DateTimeOffset Vence { get; set; }

    [Required(ErrorMessage = "El PersonaId es requerido")]
    [ForeignKey("PersonaId")]

    public int PersonaId { get; set; }

    [Required(ErrorMessage = "El Concepto es requerido")]
    public string? Concepto { get; set; }

    [Required(ErrorMessage = "El monto es requerido")]
    public int Monto { get; set; }

    [Required(ErrorMessage = "El balance es requerido")]
    public int Balance { get; set; }


}