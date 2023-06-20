using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Enums;
using PizzaProject.Storage_Waiter.Interfaces;

namespace PizzaProject
{
    public class Waiter : IStaff
    {
        public static event Action<string, Order>? OrderDelivered;

        private const ushort DELIVERY_TIME = 1000;

        private string _name;

        private Storage _storage;

        private CancellationTokenSource _cancellationTokenSource;

        public Waiter(string name, Storage storage)
        {
            _name = name;
            _storage = storage;
        }

        public string Info => _name;

        public void StartWorking()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    foreach (var order in _storage.PreparedOrders)
                    {
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }

                        if (order.Value > 0)
                        {
                            await Task.Delay(DELIVERY_TIME);
                            if (_storage.TakeOrder(order.Key) is TakeResult.SuccessfullyTaken)
                            {
                                OrderDelivered?.Invoke(_name, order.Key);
                            }
                        }
                    }
                }
            }, token);
        }

        public void StopWorking()
        {
            _cancellationTokenSource?.Cancel();
        }
    }

}