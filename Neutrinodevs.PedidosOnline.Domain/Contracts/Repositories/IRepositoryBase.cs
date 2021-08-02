using System;
using System.Collections.Generic;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        
    }
}
