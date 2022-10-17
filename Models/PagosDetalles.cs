using System.ComponentModel.DataAnnotations;

public class PagosDetalles
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El pago Id es requerido")]
    public int PagoId { get; set; }

    [Required(ErrorMessage = "El Prestamo Id es requerido")]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "El Valor Pagado es requerido")]
    public int ValorPagado { get; set; }

    public PagosDetalles()
    {
        this.Id = 0;
        this.PagoId = 0;
        this.PrestamoId = 0;
        this.ValorPagado = 0;

    }

    public PagosDetalles(int Id,int PagoId, int PrestamoId,int ValorPagado)
    {
        Id = 0;
        PagoId = 0;
        PrestamoId = 0;
        ValorPagado = 0;

    }
}