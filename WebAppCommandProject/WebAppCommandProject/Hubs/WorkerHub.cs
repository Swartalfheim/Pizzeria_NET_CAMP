using Microsoft.AspNetCore.SignalR;

namespace WebAppCommandProject.Hubs
{
    public class WorkerHub : Hub, PizzaProject.IConnector
    {

        public async Task ChefDishCompletedNotify(string time, string name, string dishName)
        {
            await Clients.All.SendAsync("Send", time, name, dishName);
        }

        public async Task WaiterDishDeliveredNotify(string time, string name, string dishName)
        {
             await Clients.All.SendAsync("Send", time, name, dishName);
        }

        public async Task CashRegisterOrderNotify(string time, string name, string order)
        {
            await Clients.All.SendAsync("Send", time, name, order);
        }

        
        public async Task NewWaiterAddNotify(string time, string name)
        {
            await Clients.All.SendAsync("Send", time, "New Waiter was Added with Name", name);
        }


        public async Task SendClient(string clientData)
        {
            await Clients.All.SendAsync("SendClient", clientData);
        }

        public async Task SendWaiter(string waiterData)
        {
            await Clients.All.SendAsync("SendWaiter", waiterData);
        }

        public async Task UpdateStateWaiter(string waiterData, string stateData)
        {
            await Clients.All.SendAsync("UpdateWaiter", waiterData, stateData);
        }

        public async Task SendChef(string chefData)
        {
            await Clients.All.SendAsync("SendChef", chefData);
        }

        public async Task UpdateStateChef(string chefData, string stateData)
        {
            await Clients.All.SendAsync("UpdateChef", chefData, stateData);
        }

        public async Task SendMenuItem(string menuItemData)
        {
            await Clients.All.SendAsync("UpdateMenu", menuItemData);
        }

        public async Task NewChefAddNotify(string time, string name)
        {
            await Clients.All.SendAsync("Send", time, "New Chef was Added with Name", name);
        }

        public async Task NewClientAddNotify(string time, string name)
        {
            await Clients.All.SendAsync("Send", time, "New Client was Added with Name", name);
        }

        public Task UpdateStateCashReg(string chefData, string stateData)
        {
            throw new NotImplementedException();
        }
    }
}
