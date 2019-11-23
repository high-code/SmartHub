using Microsoft.EntityFrameworkCore;
using SmartHub.DataAccess.Contracts;

namespace SmartHub.DataAccess.Concrete
{
  public abstract class UnitOfWorkBase<TContext> : IUnitOfWorkBase where TContext : DbContext
  {
    protected readonly TContext _context;
    private bool isDisposed;

    public UnitOfWorkBase(TContext context)
    {
      _context = context;

    }

    public int Commit() => _context.SaveChanges();

    protected virtual void Dispose(bool isDisposing)
    {
      if (isDisposed)
        return;

      if(isDisposing)
      {
        _context.Dispose();
      }

      isDisposed = true;
    }


    public void Dispose()
    {
      Dispose(true);

      
    }
  }
}
