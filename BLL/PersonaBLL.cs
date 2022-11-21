using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PersonasBLL
{
    private Contexto _contexto;

    public PersonasBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Personas.AnyAsync(p => p.PersonaId == id);
    }

    public async Task<bool> Guardar(Personas persona)
    {
        var existe = await Existe(persona.OcupacionId);

        if (!existe)
            return await this.Insertar(persona);
        else
            return await this.Modificar(persona);
    }

    private async Task<bool> Insertar(Personas persona)
    {
        await _contexto.Personas.AddAsync(persona);

        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    private async Task<bool> Modificar(Personas persona)
    {
        _contexto.Entry(persona).State = EntityState.Modified;

        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<bool> Eliminar(Personas persona)
    {
        _contexto.Entry(persona).State = EntityState.Deleted;
        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<Personas?> Buscar(int id)
    {
        var ocupacion = await _contexto.Personas
                .Where(p => p.PersonaId == id)
                .AsTracking()
                .SingleOrDefaultAsync();

        return ocupacion;
    }

    public async Task<List<Personas>> GetList(Expression<Func<Personas, bool>> Criterio)
    {
        return await _contexto.Personas
            .Where(Criterio)
            .AsTracking()
            .ToListAsync();
    }
}