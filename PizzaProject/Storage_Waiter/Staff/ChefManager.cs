using System.Collections.Concurrent;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Storage_Waiter.Interfaces;

namespace PizzaProject.Storage_Waiter.Staff
{
    public class ChefManager : IStaff
    {
        private string _name;
        private readonly object _chefLock = new object();
        private BlockingCollection<string> _orderQueue = new BlockingCollection<string>();
        public List<Chef> Chefs { get; set; }

        public ChefManager(string name, List<Chef> chefsIn)
        {
            _name = name;
            Chefs = chefsIn;
            Task.Factory.StartNew(ProcessOrders, TaskCreationOptions.LongRunning);
        }

        public Chef? GetChefByName(string name)
        {
            return Chefs.Find(chef => chef.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void AddOrder(Order order)
        {
            if (_orderQueue.IsAddingCompleted)
            {
                StartProcessingOrders();
            }
            foreach (var item in order.FoodSet)
            {
                _orderQueue.Add(item.Key.Name);
            }
        }

        private void ProcessOrders()
        {
            foreach (var order in _orderQueue.GetConsumingEnumerable())
            {
                Chef? freeChef = null;
                lock (_chefLock)
                {
                    freeChef = Chefs.Find(chef => !chef.IsBusy && chef.Recipes.ContainsKey(order));
                }

                if (freeChef != null)
                {
                    freeChef.IsBusy = true;
                    Task.Run(() =>
                    {
                        try
                        {
                            freeChef.Cook(order);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            freeChef.IsBusy = false;
                            _orderQueue.Add(order);
                        }
                    });
                }
                else
                {
                    _orderQueue.Add(order);
                }
            }
        }
        public void StartProcessingOrders() // Відновлення роботи ChefManager
        {
            _orderQueue = new BlockingCollection<string>();
            Task.Factory.StartNew(ProcessOrders, TaskCreationOptions.LongRunning);
        }

        public void StopProcessingOrders() // зупинка процесу роботи ChefManager
        {
            _orderQueue.CompleteAdding();
        }
        public string Info
        {
            get
            {
                return _name;
            }
        }
    }
}
