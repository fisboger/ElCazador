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
            Console.WriteLine("NEW DATASTORE");
            HostingEnvironment = hostingEnvironment;

            Stores = new ConcurrentDictionary<Type, object>();
        }

        public IDataStoreObject<T> Get<T>() where T : IDataObject
        {
            var type = typeof(T);
            var storeExists = Stores.ContainsKey(typeof(T));

            if (!storeExists)
            {
                Console.WriteLine("New {0} store", type.Name);
                var store = new JsonDataStoreObject<T>(HostingEnvironment);
                Stores.TryAdd(type, store);

                return store;
            }
            else
            {
                Stores.TryGetValue(type, out object result);

                return (IDataStoreObject<T>)result;
            }
        }
    }
}