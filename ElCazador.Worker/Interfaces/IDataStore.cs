using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Interfaces
{
    public interface IDataStore
    {
         IDataStoreObject<T> Get<T>() where T : IDataObject;
    }
}