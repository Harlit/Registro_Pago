using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pagos
{
    [Key]
    public int PagoId { get; set; }
    [Required(ErrorMessage = "La Fecha es requerida")]
    public DateTime Fecha { get; set; }
    [Required(ErrorMessage = "La PersonaId es requerida")]
    public int PersonaId { get; set; }
    [Required(ErrorMessage = "El Concepto es requerida")]
    public string? Concepto { get; set; }
    [Required(ErrorMessage = "El Monto es requerida")]
    public int Monto { get; set; }

    [ForeignKey("Id")]

    public virtual List<PagosDetalle> PagosDetalles { get; set; }

    public class PagosDetalle
    {

        [Key]
        public int Id { get; set; }
        public int PagoId { get; set; }

        [Required(ErrorMessage = "El Prestamo Id Es Obligatorio")]
        public int PrestamosId { get; set; }

        public double ValorPagado { get; set; }

    }

}
