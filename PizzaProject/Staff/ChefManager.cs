using System.Collections.Concurrent;

namespace PizzaProject.Staff
{
    public class ChefManager : IStaff
    {
        private string _name;
        private readonly object _chefLock = new object();
        private BlockingCollection<string> _orderQueue = new BlockingCollection<string>();
        public List<Chef> Chefs { get; set; } = new List<Chef>();

        public ChefManager(string name)
        {
            _name = name;
            Task.Factory.StartNew(ProcessOrders, TaskCreationOptions.LongRunning);
        }

        public Chef GetChefByName(string name)
        {
            return Chefs.Find(chef => chef.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void AddOrder(string order)
        {
            if (_orderQueue.IsAddingCompleted)
            {
                StartProcessingOrders();
            }

            _orderQueue.Add(order);
        }

        private void ProcessOrders()
        {
            foreach (var order in _orderQueue.GetConsumingEnumerable())
            {
                Chef freeChef = null;
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
