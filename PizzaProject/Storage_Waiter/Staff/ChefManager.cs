using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Storage_Waiter.Interfaces;

namespace PizzaProject.Storage_Waiter.Staff
{
    public class ChefManager : IStaff
    {
        private string _name;
        private readonly object _chefLock = new ();
        private BlockingCollection<IOffer> _dishesList = new();
        private Dictionary<Order, List<IOffer>> _ordersBeingPrepared = new();
        private Storage _storage = new ();
        public List<Chef> Chefs { get; set; }

        public ChefManager(string name, List<Chef> chefsIn, Storage storage)
        {
            _name = name;
            Chefs = chefsIn;
            _storage = storage;
            Task.Factory.StartNew(ProcessOrders, TaskCreationOptions.LongRunning);
        }

        public Chef? GetChefByName(string name)
        {
            return Chefs.Find(chef => chef.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void AddOrder(Order order)
        {
            if (_dishesList.IsAddingCompleted)
            {
                StartProcessingOrders();
            }

            _ordersBeingPrepared[order] = new List<IOffer>();
            foreach (var item in order.FoodSet)
            {
                _dishesList.Add(item.Key);
                _ordersBeingPrepared[order].Add(item.Key);
            }
        }

        private void ProcessOrders()
        {
            foreach (var order in _dishesList.GetConsumingEnumerable())
            {
                Chef? freeChef = null;
                lock (_chefLock)
                {
                    freeChef = Chefs.Find(chef => !chef.IsBusy && chef.Recipes.Contains(order.Recipe));
                }

                if (freeChef != null)
                {
                    freeChef.IsBusy = true;
                    Task.Run(() =>
                    {
                        try
                        {
                            freeChef.Cook(order);
                            lock (_ordersBeingPrepared)
                            {
                                var cookedOrder = _ordersBeingPrepared.First(o => o.Value.Contains(order)).Key;
                                _ordersBeingPrepared[cookedOrder].Remove(order);

                                if (!_ordersBeingPrepared[cookedOrder].Any()) // якщо всі блюда замовлення пригтовані
                                {
                                    _storage.PutOrder(cookedOrder); // додати замовлення до Storage коли всі блюда приготовлені
                                    _ordersBeingPrepared.Remove(cookedOrder);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            freeChef.IsBusy = false;
                            _dishesList.Add(order);
                        }
                    });
                }
                else
                {
                    _dishesList.Add(order);
                }
                Thread.Sleep(1000);
            }
        }
        public void StartProcessingOrders() // Відновлення роботи ChefManager
        {
            _dishesList = new BlockingCollection<IOffer>();
            Task.Factory.StartNew(ProcessOrders, TaskCreationOptions.LongRunning);
        }

        public void StopProcessingOrders() // зупинка процесу роботи ChefManager
        {
            _dishesList.CompleteAdding();
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
