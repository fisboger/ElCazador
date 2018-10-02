using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ElCazador.Worker.DataStore;
using ElCazador.Worker.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace ElCazador.Web.DataStore
{
    public class JsonDataStore : IDataStore
    {
        private ConcurrentDictionary<Type, object> Stores { get; set; }
        private IHostingEnvironment HostingEnvironment { get; set; }

        public JsonDataStore(
            IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;

            Stores = new ConcurrentDictionary<Type, object>();
        }

        public IDataStoreObject<T> Get<T>() where T : IDataObject
        {
            return (IDataStoreObject<T>)Stores.GetOrAdd(typeof(T), new JsonDataStoreObject<T>(HostingEnvironment));
        }
    }
}