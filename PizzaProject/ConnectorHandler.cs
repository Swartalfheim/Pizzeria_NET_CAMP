
using Newtonsoft.Json;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Storage_Waiter.Staff;


namespace PizzaProject
{
    public static class ConnectorHandler
    {
        private static IConnector _connector { get; set; }

        public static void SetConnector(IConnector connector)
        {
            _connector = connector;
        }

        public static void ChefDishCompletedNotify(string name, string dishName)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            _connector.ChefDishCompletedNotify(currentTime, $"Chef {name}", $" COOKED {dishName}");
            Console.WriteLine($"{currentTime} - Chef {name} COOKED {dishName}");
        }

        public static void WaiterDishDeliveredNotify(string name, Order dishName)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            _connector.WaiterDishDeliveredNotify(currentTime, $"Waiter {name}", $" DELIVERED {dishName}");
            Console.WriteLine($"{currentTime} - Waiter {name} DELIVERED {dishName}");
        }

        public static void CashRegisterOrderNotify(Order order)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            _connector.CashRegisterOrderNotify(currentTime, "CashRegister", $"\tNew Order Formed: {string.Join(" ", order)}");
            Console.WriteLine($"{currentTime} - \tNew Order Formed: {string.Join(" ", order)}");
        }

        public static void NewWaiterAddNotify(Waiter waiter)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            _connector.NewWaiterAddNotify(currentTime, waiter.Info);
        }

        public static void NewChefAddNotify(Chef chef)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            _connector.NewChefAddNotify(currentTime, chef.Info);
        }

        public static void SendClientData(Customer client)
        {
            string clientData = JsonConvert.SerializeObject(client);
            _connector.SendClient(clientData);
        }

        public static void SendWaiterData(Waiter waiter)
        {
            string waiterData = JsonConvert.SerializeObject(waiter);
            _connector.SendWaiter(waiterData);
        }

        public static void UpdateWaiterData(Waiter waiter, Order order)
        {
            string waiterData = JsonConvert.SerializeObject(waiter);
            string stateData = $"Delivered [ID:{order.Id}] ";
            _connector.UpdateStateWaiter(waiterData, stateData);
        }

        public static void SendChefData(Chef chef)
        {
            string chefData = JsonConvert.SerializeObject(new { Id = chef.Id, Name = chef.Name });
            _connector.SendChef(chefData);
        }

        public static void UpdateChefData(Chef chef, string offer)
        {
            string chefData = JsonConvert.SerializeObject(new { Id = chef.Id, Name = chef.Name });
            string stateData = $"COOKED [{offer}] ";
            _connector.UpdateStateChef(chefData, stateData);
        }

        public static void UpdateMenuItemData(KeyValuePair<IOffer, decimal> dish)
        {
            var res = new { Dish = dish.Key, Price = dish.Value };

            string result = JsonConvert.SerializeObject(res);
            _connector.SendMenuItem(result);
        }

        public static void NewClientAddNotify(Customer client)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");

            _connector.NewClientAddNotify(currentTime, client.Name);
        }
    }
}
