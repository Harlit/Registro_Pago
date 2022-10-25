using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class OcupacionesBLL
{
    private Contexto _contexto;

    public OcupacionesBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Ocupaciones.AnyAsync(o => o.OcupacionId == id);
    }

    public async Task<bool> Guardar(Ocupaciones ocupacion)
    {
        var existe = await Existe(ocupacion.OcupacionId);

        if (!existe)
            return await this.Insertar(ocupacion);
        else
            return await this.Modificar(ocupacion);
    }

    private async Task<bool> Insertar(Ocupaciones ocupacion)
    {
        await _contexto.Ocupaciones.AddAsync(ocupacion);

        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    private async Task<bool> Modificar(Ocupaciones ocupacion)
    {
        _contexto.Entry(ocupacion).State = EntityState.Modified;

        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<bool> Eliminar(Ocupaciones ocupacion)
    {
        _contexto.Entry(ocupacion).State = EntityState.Deleted;
        var cantidad = await _contexto.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<Ocupaciones?> Buscar(int ocupacionId)
    {
        var ocupacion = await _contexto.Ocupaciones
                .Where(o => o.OcupacionId == ocupacionId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

        return ocupacion;
    }

    public async Task<List<Ocupaciones>> GetList(Expression<Func<Ocupaciones, bool>> Criterio)
    {
        return await _contexto.Ocupaciones
            .Where(Criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}