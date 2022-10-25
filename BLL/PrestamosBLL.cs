using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PrestamosBLL
{
    private Contexto _contexto;

    public PrestamosBLL(Contexto contexto)
    {
        this._contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Prestamos.AnyAsync(p => p.PrestamoId == id);
    }

    public async Task<bool> Guardar(Prestamos prestamo)
    {
        var existe = await Existe(prestamo.PrestamoId);

        if (!existe)
            return await this.Insertar(prestamo);
        else
            return await this.Modificar(prestamo);
    }

    private async Task<bool> Insertar(Prestamos prestamo)
    {
        await _contexto.Prestamos.AddAsync(prestamo);

        //afectar el balance de la persona
        var persona = await _contexto.Personas.FindAsync(prestamo.PersonaId);
        persona!.Balance += prestamo.Monto;

        prestamo.Balance = prestamo.Monto;
        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    private async Task<bool> Modificar(Prestamos prestamoActual)
    {
        //descontar el monto anterior
        var prestamoAnterior = await _contexto.Prestamos
            .Where(p => p.PrestamoId == prestamoActual.PrestamoId)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        var personaAnterior = await _contexto.Personas.FindAsync(prestamoAnterior!.PersonaId);
        personaAnterior!.Balance -= prestamoAnterior.Monto;

        _contexto.Entry(prestamoActual).State = EntityState.Modified;

        //afectar el monto nuevo
        var persona = await _contexto.Personas.FindAsync(prestamoActual.PersonaId);
        persona!.Balance += prestamoActual.Monto;

        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<bool> Eliminar(Prestamos prestamo)
    {
        //restar el balance a la persona
        var persona = await _contexto.Personas.FindAsync(prestamo!.PersonaId);
        persona!.Balance -= prestamo.Monto;

        _contexto.Entry(prestamo).State = EntityState.Deleted;
        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<Prestamos?> Buscar(int id)
    {
        var ocupacion = await _contexto.Prestamos
                .Where(p => p.PrestamoId == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

        return ocupacion;
    }

    public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> Criterio)
    {
        return await _contexto.Prestamos
            .Where(Criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}