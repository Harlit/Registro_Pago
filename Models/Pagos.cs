using System.ComponentModel.DataAnnotations;


public class Pagos
{
    [Key]
    public int PagoId { get; set; }
    [Required(ErrorMessage ="La Fecha es requerida")]
    public DateTime Fecha {get; set;}
    [Required(ErrorMessage ="La PersonaId es requerida")]
    public int PersonaID {get; set;}
    [Required(ErrorMessage ="El Concepto es requerida")]
    public string? Concepto {get; set;}
    [Required(ErrorMessage ="El Monto es requerida")]
    public int Monto {get; set;}
}