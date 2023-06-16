using System.Collections.Concurrent;

namespace PizzaProject.Staff
{
    public class ChefManager : IStaff
    {
        private string _name;
        private readonly object _chefLock = new object();
        private ConcurrentQueue<string> _orderQueue = new ConcurrentQueue<string>();
        private Thread _orderProcessingThread;

        public List<Chef> Chefs { get; set; } = new List<Chef>();

        public ChefManager(string name)
        {
            _orderProcessingThread = new Thread(ProcessOrders);
            _orderProcessingThread.Start();
            _name = name;
        }

        public Chef GetChefByName(string name)
        {
            return Chefs.Find(chef => chef.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void AddOrder(string order)
        {
            _orderQueue.Enqueue(order);
        }

        private void ProcessOrders()
        {
            while (true)
            {
                if (_orderQueue.TryDequeue(out string order))
                {
                    Chef freeChef = null;
                    lock (_chefLock)
                    {
                        freeChef = Chefs.Find(chef => !chef.IsBusy && chef.Recipes.ContainsKey(order));
                    }

                    if (freeChef != null)
                    {
                        freeChef.IsBusy = true;
                        new Thread(() =>
                        {
                            try
                            {
                                freeChef.Cook(order);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                freeChef.IsBusy = false;
                                _orderQueue.Enqueue(order);
                            }
                        }).Start();
                    }
                    else
                    {
                        _orderQueue.Enqueue(order);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
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
