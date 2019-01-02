using System;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs.Actions
{
    public class UserHubActions : IHubActions<User>
    {
        private IHubContext<UserHub> UserHubContext { get; set; }

        public UserHubActions(IHubContext<UserHub> userHubContext)
        {
            UserHubContext = userHubContext;
        }
        public async Task Add(User user)
        {
            await UserHubContext.Clients.All.SendAsync("AddUser", user);
        }

        public async Task Edit(User user)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}