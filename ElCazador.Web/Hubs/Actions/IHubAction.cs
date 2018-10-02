using System.Threading.Tasks;

namespace ElCazador.Web.Hubs.Actions
{
    public interface IHubAction<T> where T : class
    {
         Task Add(T entity);
         Task Edit(T entity);
    }
}