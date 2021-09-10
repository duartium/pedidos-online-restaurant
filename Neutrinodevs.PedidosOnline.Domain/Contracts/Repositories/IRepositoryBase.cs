using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> //: IDisposable //where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        
    }
}
