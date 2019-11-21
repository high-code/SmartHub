using System;

namespace SmartHub.DataAccess.Contracts
{
  public interface IUnitOfWorkBase : IDisposable
  {
    int Commit();
  }
}
