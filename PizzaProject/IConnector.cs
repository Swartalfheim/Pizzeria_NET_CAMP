
namespace PizzaProject
{
    public interface IConnector
    {
        public Task ChefDishCompletedNotify(string time, string name, string dishName);
        public Task WaiterDishDeliveredNotify(string time, string name, string dishName);
        public Task CashRegisterOrderNotify(string time, string name, string order);
        public Task NewWaiterAddNotify(string time, string name);
        public Task NewChefAddNotify(string time, string name);
        public Task NewClientAddNotify(string time, string name);

        public Task SendClient(string clientData);
        public Task SendWaiter(string waiterData);
        public Task UpdateStateWaiter(string waiterData, string stateData);

        public Task SendChef(string chefData);
        public Task UpdateStateChef(string chefData, string stateData);

        public Task UpdateStateCashReg(string chefData, string stateData);

        public Task SendMenuItem(string menuItemData);
    }
}
