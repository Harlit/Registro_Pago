using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PagosBLL
{
    private Contexto _contexto;

    public PagosBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int pagoId)
    {
        return _contexto.Pagos.Any(o => o.PagoId == pagoId);
    }

    private bool Insertar(Pagos pago)
    {
        _contexto.Pagos.Add(pago);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(Pagos pago)
    {
        bool paso = false;

        try
        {
            _contexto.Database.ExecuteSqlRaw($"Delete FROM PagosDetalles where PagoId={pago.PagoId}");

            foreach (var anterior in pago.PagosDetalles)
            {
                _contexto.Entry(anterior).State = EntityState.Added;
            }

            _contexto.Entry(pago).State = EntityState.Modified;

            paso = _contexto.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }

        finally
        {
            _contexto.Dispose();
        }

        return paso;
    }

    public bool Guardar(Pagos pago)
    {
        if (!Existe(pago.PagoId))
            return this.Insertar(pago);
        else
            return this.Modificar(pago);
    }

    public bool Eliminar(Pagos pago)
    {
        _contexto.Entry(pago).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }

    public Pagos Buscar(int id)
    {
      
        Pagos pago = new Pagos();

        // Pagos pago;

        try
        {
            pago = _contexto.Pagos.Include(x => x.PagosDetalles)
            .Where(p => p.PagoId == id)
            .SingleOrDefault();
        }
        catch (Exception)
        {
            throw;
        }

        finally{
            _contexto.Dispose();
        }
        return pago;
    }
    public List<Pagos> GetList(Expression<Func<Pagos, bool>> Criterio)
    {
        return _contexto.Pagos
            .AsNoTracking()
            .Where(Criterio)
            .ToList();
    }

}