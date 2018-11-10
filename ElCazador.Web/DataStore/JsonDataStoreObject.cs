using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElCazador.Worker.DataStore;
using ElCazador.Worker.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace ElCazador.Web.DataStore
{
    public class JsonDataStoreObject<T> : IDataStoreObject<T> where T : IDataObject
    {
        public string Name { get; private set; } = typeof(T).Name;

        private string FileName => string.Format("{0}.json", Name);
        private string FullPath => string.Format("{0}/{1}", HostingEnvironment.ContentRootPath, FileName);

        private DateTime LastCommit { get; set; }
        private DateTime LastRead { get; set; }

        private IHostingEnvironment HostingEnvironment { get; set; }
        private ConcurrentDictionary<Guid, T> Store { get; set; }

        private SemaphoreSlim Semaphore = new SemaphoreSlim(1);

        public JsonDataStoreObject(
            IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;

            Initialize();
            StartWatcher();
        }

        private void Initialize()
        {
            Store = new ConcurrentDictionary<Guid, T>();

            if (File.Exists(FullPath))
            {
                LastCommit = File.GetLastWriteTimeUtc(FullPath);
                var content = File.ReadAllText(FullPath);

                if (!string.IsNullOrWhiteSpace(content))
                {
                    var entities = Deserialize(content);
                    Store = new ConcurrentDictionary<Guid, T>(entities.ToDictionary(entity => entity.ID));
                }
            }
        }

        private void StartWatcher()
        {
            var watcher = new FileSystemWatcher(Path.GetDirectoryName(FullPath), Path.GetFileName(FullPath));

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            var lastWriteTime = File.GetLastWriteTimeUtc(FullPath);

            if (lastWriteTime != LastCommit)
            {
                Initialize();
            }
        }


        /// <summary>
        /// Get all T entities
        /// </summary>
        public IEnumerable<T> All => Store.Values;

        /// <summary>
        /// Get the T entity by the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(Guid key)
        {
            if (Store.TryGetValue(key, out T value))
            {
                return value;
            }

            return default(T);
        }

        /// <summary>
        /// Add the T entity to the store and commit changes to the disk
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Add(T entity)
        {
            await AddOrEditInternal(entity);
        }

        /// <summary>
        /// Delete the T entity from the store and commit changes to the disk
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete(T entity)
        {
            await DeleteInternal(entity);
        }
        /// <summary>
        /// Edit the T entity from the store and commit changes to the disk
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Edit(T entity)
        {
            await AddOrEditInternal(entity);
        }

        private async Task AddOrEditInternal(T entity)
        {
            Store.AddOrUpdate(entity.ID, entity, (key, oldValue) => entity);

            await Commit();
        }

        private async Task DeleteInternal(T entity)
        {
            var result = Store.TryRemove(entity.ID, out T value);

            if (result == true)
            {
                await Commit();
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Commit changes to the disk
        /// </summary>
        /// <returns></returns>
        private async Task Commit()
        {
            await Semaphore.WaitAsync();

            try
            {
                // We don't want this to be async, as then we don't get the right lastwritetime
                File.WriteAllText(FullPath, Serialize());
                LastCommit = File.GetLastWriteTimeUtc(FullPath);
            }
            finally
            {
                Semaphore.Release();
            }
        }

        private string Serialize()
        {
            return JsonConvert.SerializeObject(Store.Values, Formatting.None);
        }

        private IEnumerable<T> Deserialize(string content)
        {
            return JsonConvert.DeserializeObject<List<T>>(content);
        }
    }
}