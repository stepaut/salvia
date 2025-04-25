using System.Collections.Generic;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

public interface IGetAllRepository<TItem, TCCommonKey>
{
    Task<ICollection<TItem>> GetAll(TCCommonKey commonKey); 
}

public interface IRepository<TItem, TKey>
{
    Task<TItem?> TryGetItem(TKey id);
    Task<TItem> Create(TItem item);
    Task Update(TItem item);
    Task Delete(TKey id);
    Task Save();
}

