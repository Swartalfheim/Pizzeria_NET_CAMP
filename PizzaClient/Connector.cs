
namespace PizzaClient
{
    internal class Connector : PizzaProject.IConnector
    {
        public Task CashRegisterOrderNotify(string time, string name, string order)
        {
            throw new NotImplementedException();
        }

        public Task ChefDishCompletedNotify(string time, string name, string dishName)
        {
            throw new NotImplementedException();
        }

        public Task WaiterDishDeliveredNotify(string time, string name, string dishName)
        {
            throw new NotImplementedException();
        }
    }
}
