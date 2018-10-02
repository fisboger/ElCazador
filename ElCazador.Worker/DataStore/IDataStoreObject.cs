using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElCazador.Worker.DataStore
{
    public interface IDataStoreObject<T> where T : IDataObject
    {
        IEnumerable<T> All { get; }
        T Get(Guid key);
        Task Add(T entity);
        Task Delete(T entity);
        Task Edit(T entity);
    }
}