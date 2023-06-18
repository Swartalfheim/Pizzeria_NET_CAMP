using PizzaProject.Enums;
using PizzaProject.Storage_Waiter.Interfaces;

namespace PizzaProject
{
    public class Waiter : IStaff
    {
        public static event Action<string, string>? DishDelivered;

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

            Task.Run(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine($"{_name} has stopped working.");
                        break;
                    }

                    foreach (var dish in _storage.PreparedDishes)
                    {
                        if (dish.Value > 0)
                        {
                            Task.Delay(2000).Wait();
                            if (_storage.TakeDish(dish.Key) is TakeResult.SuccessfullyTaken)
                            {
                                DishDelivered?.Invoke(_name, dish.Key);
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