using Microsoft.AspNetCore.SignalR;
using PizzaProject.Dishes_Orders.Implementations;
using Pizzaria_Library;
using WebAppCommandProject.Hubs;
using WebAppCommandProject.Model;
using WebAppCommandProject.Model.ProjectDTO;

namespace WebAppCommandProject.Services
{
    public class WorkerServices : BackgroundService
    {
        private readonly ILogger<WorkerServices> _logger;
        private readonly PizzaProject.IConnector _hubContext;


        PizzariaLibrary _pizzaProject;

        private Thread _pizza;

        public WorkerServices(ILogger<WorkerServices> logger, WorkerHub hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;

            _pizzaProject = new PizzariaLibrary(_hubContext);

            _pizzaProject.pizzaApp.PizzaInit();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _pizza = new Thread(_pizzaProject.pizzaApp.Start);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the background task.");
            }

        }

        public void AddWaiter(string waiterName)
        {
            _pizzaProject.pizzaApp.AddWaiter(waiterName);
        }

        public void AddChef(ChefDto chef)
        {
            _pizzaProject.pizzaApp.SetChefInAdmin(chef.Name, chef.CategoryId);
        }

        public void AddClient(ClientFromAdminDto client)
        {
            _pizzaProject.pizzaApp.AddClientAdmin(client.Name, client.VipLevel, client.Amount, client.PaymentCategory);
        }

        public void AddDish(CreateDishDto dish)
        {
            int[] ingredient = dish.IngrDict.Keys.ToArray();
            uint[] countIngredients = dish.IngrDict.Values.ToArray();
            _pizzaProject.pizzaApp.AddDishInMenu(dish.Name, dish.Description, ingredient, countIngredients, dish.TimePrepare, dish.Size, dish.Dough, dish.Price);
        }

        public void AddOrder(OrderDto order)
        {
            List<int> dishesId = new List<int>();
            int[] dish = order.DishDict.Keys.ToArray();
            int[] countDish = order.DishDict.Values.ToArray();

            for (int i = 0; i < dish.Length; i++)
            {
                for (int j = 0; j < countDish[i]; j++)
                {
                    dishesId.Add(dish[i]);
                }
            }
            _pizzaProject.pizzaApp.AddClient(order.Name, order.Amount, order.Payment, dishesId.ToArray());
        }

        public string GetMenu()
        {
            return _pizzaProject.pizzaApp.GetMenu();
        }

        public string GetCashRegisters()
        {
            return _pizzaProject.pizzaApp.GetCashRegister();
        }

        public string GetIngredients()
        {
            return _pizzaProject.pizzaApp.GetIngredients();
        }


        public void ManualControl(CommandManual command)
        {

            switch (command)
            {
                case CommandManual.Start:
                    _pizza.Start();
                    break;
                //case CommandManual.Stop:
                    
                //    break;
                case CommandManual.Pause:
                    _pizzaProject.pizzaApp.Pause();
                    break;
                case CommandManual.Continue:
                    _pizzaProject.pizzaApp.Continue();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}
