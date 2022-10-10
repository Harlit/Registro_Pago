using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext
{
    public DbSet<Pagos> Pagos { get; set; }
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }
}