using System;
using Xunit;
using ElCazador.Worker;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Tools;
using Moq;
using ElCazador.Worker.Interfaces;

namespace ElCazador.Test
{
    public class WorkerTest
    {
        private Mock<IWorkerController> WorkerControllerMock;

        public WorkerTest()
        {
            WorkerControllerMock = new Mock<IWorkerController>();

            WorkerControllerMock.Setup((x) => x.WorkerSettings.ImpacketExamplesPath).Returns("/Users/elpatron/Documents/Tools/impacket/examples/");

            
        }

        [Fact]
        public void TestLsassDump()
        {
            var target = new Target
            {
                Hostname = "PWNABLE10",
                IP = "10.64.13.134",
                Dumped = false,
                Timestamp = DateTime.UtcNow
            };

            var user = new User
            {
                Username = "Bente",
                Domain = "PWNTHIS",
                Hash = "Headshot1",
                PasswordType = "ClearText"
            };

            var lsassDumpTool = new LsassDumpTool(WorkerControllerMock.Object, "LsassDump");

            Assert.Equal("/Users/elpatron/Documents/Tools/impacket/examples/", WorkerControllerMock.Object.WorkerSettings.ImpacketExamplesPath);

            lsassDumpTool.Run(target, user);
        }
    }
}
