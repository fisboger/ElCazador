using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ElCazador.Web.DataStore;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Newtonsoft.Json;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace ElCazador.Test.Web
{
    public class JsonDataStoreTest : IDisposable
    {
        private string Path = @"/Users/elpatron/Documents/Projects/ElCazador/ElCazador.Test";
        private Mock<IHostingEnvironment> HostingEnvironmentMock;

        private IDataStore DataStore;
        public JsonDataStoreTest()
        {
            File.Delete(string.Format("{0}/{1}", Path, "Target.json"));
            File.Delete(string.Format("{0}/{1}", Path, "User.json"));

            HostingEnvironmentMock = new Mock<IHostingEnvironment>();

            HostingEnvironmentMock.Setup((x) => x.ContentRootPath).Returns(Path);
            DataStore = new JsonDataStore(HostingEnvironmentMock.Object);
        }

        #region Data
        Target Target = new Target
        {
            ID = Guid.NewGuid(),
            Hostname = "Test-host",
            IP = "192.168.0.2",
            Dumped = false,
            Timestamp = DateTime.UtcNow
        };

        User User = new User
        {
            ID = Guid.NewGuid(),
            IPAddress = "192.168.0.2",
            Username = "TestUser",
            Hash = "000000000000000000000000000000:000000000000000000000000000000",
            HashcatFormat = ""
        };
        #endregion

        [Fact]
        public void TestAdd()
        {
            var targetStore = DataStore.Get<Target>();
            var userStore = DataStore.Get<User>();

            targetStore.Add(Target);
            userStore.Add(User);

            var allTargets = targetStore.All;
            var allUsers = userStore.All;

            Assert.Single(allTargets);
            Assert.Single(allUsers);

            var target = targetStore.Get(Target.ID);
            var user = userStore.Get(User.ID);

            Assert.NotNull(target);
            Assert.NotNull(user);

            Assert.True(File.Exists(string.Format("{0}/{1}", Path, "Target.json")));
            Assert.True(File.Exists(string.Format("{0}/{1}", Path, "User.json")));
        }

        // [Fact]
        // public void ParallelTestAdd()
        // {
        //     var targetStore = DataStore.Get<Target>();

        //     var parallel = Parallel.For(0, 1000, new ParallelOptions
        //     {
        //         MaxDegreeOfParallelism = 4
        //     }, (index) =>
        //     {
        //         var target = new Target
        //         {
        //             ID = Guid.NewGuid(),
        //             Hostname = "Test-host",
        //             IP = "192.168.0.2",
        //             Dumped = false,
        //             Timestamp = DateTime.UtcNow
        //         };

        //         targetStore.Add(target);
        //     });

        //     // THis is needed to not fuck up all other tests as it will not delete the file...
        //     while (!parallel.IsCompleted)
        //     {
        //         Thread.Sleep(50);
        //     }

        //     Assert
        // }

        [Fact]
        public void TestDelete()
        {
            var targetStore = DataStore.Get<Target>();

            targetStore.Add(Target);

            targetStore.Delete(Target);

            Assert.Empty(targetStore.All);
            Assert.Equal("[]", File.ReadAllText(string.Format("{0}/{1}", Path, "Target.json")));
        }

        [Fact]
        public void TestEdit()
        {
            var targetStore = DataStore.Get<Target>();

            targetStore.Add(Target);

            Target.Dumped = true;
            Target.Hostname = "This is a test";
            targetStore.Edit(Target);

            var target = targetStore.Get(Target.ID);

            Assert.Equal(Target.Dumped, target.Dumped);

            var savedJson = File.ReadAllText(string.Format("{0}/{1}", Path, "Target.json"));
            var generatedJson = JsonConvert.SerializeObject(new List<Target> { Target }, Formatting.None);

            Assert.Equal(savedJson, generatedJson);
        }

        [Fact]
        public void TestJsonIntegrity()
        {
            var targetStore = DataStore.Get<Target>();

            targetStore.Add(Target);

            var target = targetStore.Get(Target.ID);

            var savedJson = File.ReadAllText(string.Format("{0}/{1}", Path, "Target.json"));
            var generatedJson = JsonConvert.SerializeObject(new List<Target> { target }, Formatting.None);

            Assert.Equal(savedJson, generatedJson);
        }

        public void Dispose()
        {
            File.Delete(string.Format("{0}/{1}", Path, "Target.json"));
            File.Delete(string.Format("{0}/{1}", Path, "User.json"));
        }
    }
}