using System.ComponentModel.DataAnnotations;


public class Ocupaciones
{
    [Key]
    public int OcupacionId {get; set;}
    [Required(ErrorMessage ="La descripcion es requerida")]
    public string? Descripcion {get; set;}
    
    public int Salario {get; set;}
}