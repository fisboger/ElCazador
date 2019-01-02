using System.Threading.Tasks;
using ElCazador.Worker.DataStore;

namespace ElCazador.Web.Hubs.Actions
{
    public interface IHubActions<T> where T : IDataObject
    {
         Task Add(T entity);
         Task Edit(T entity);
    }
}