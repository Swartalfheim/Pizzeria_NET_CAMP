using PizzaProject.Administration;
using PizzaProject.Costumer_Payment;
using PizzaProject.Costumer_Payment.CashRegisters;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.CustomersGeneration;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Menu_Loyality.Loyality;
using PizzaProject.Menu_Loyality.Menu;
using PizzaProject.Simulation;
using PizzaProject.Simulator_Generator.CustomersGeneration;
using PizzaProject.Storage_Waiter.Interfaces;
using PizzaProject.Storage_Waiter.Staff;
using Newtonsoft.Json;
using PizzaProject.Dishes_Orders.Abstractions;

using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject
{
    public class Program
    {
        private Admin _admin;
        private Storage _productStorage;
        private Menu _menu;
        private ChefManager _chefManager;
        private Simulator _simulator;
        private PizzeriaData _pizzeriaData;
        private Manager _manager;


        static void Main(string[] args)
        {
            
        }

        public void Start()
        {
            _simulator.StartSimulation();
        }

        public void Continue()
        {
            _simulator.ContinueSimulation();
        }

        public void Pause()
        {
            _simulator.StopSimulation();
        }

        public void PizzaInit()
        {
            var customerGenerator = new CustomerGenerator(new DefaultCustomerGenerator(10));


            HashSet<Wallet> wallets = new HashSet<Wallet>();
            wallets.Add(new Wallet(400m, Wallet.PaymentCategory.Card));
            wallets.Add(new Wallet(100m, Wallet.PaymentCategory.Cash));
            wallets.Add(new Wallet(200m, Wallet.PaymentCategory.PaymentService));

            HashSet<ICashRegister> cashRegisters = new HashSet<ICashRegister>();

            _menu = Menu.GetInstance();

            cashRegisters.Add(new CashRegister(_menu, wallets));
            cashRegisters.Add(new CashRegister(_menu, wallets));

            foreach (var item in cashRegisters)
            {
                item.NewOrderApperiance += ConnectorHandler.CashRegisterOrderNotify;
            }

            _productStorage = new Storage(Filler.GetIngredientForStorage());

            LoyaltyProgram loyaltyProgram = new LoyaltyProgram();

            List<Chef> chefs = new List<Chef>
            {
                new Chef("Anton", _productStorage, Filler.GetAllCategories()),
                new Chef("Oleg", _productStorage, Filler.GetCategoryForFirstChef()),
                new Chef("Ivan", _productStorage, Filler.GetCategoryForFirstChef())
            };


            //chefs.ForEach(x => x.DishPrepared += PrintChefNotify);
            //ChefNotifyDishCompleted
            chefs.ForEach(x => x.DishPrepared += ConnectorHandler.ChefDishCompletedNotify);

           

            HashSet<IStaff> staff = new HashSet<IStaff>();
            staff = chefs.Select(x => (IStaff)x).ToHashSet();

            _chefManager = new ChefManager("Big Bos", chefs, _productStorage);

            Waiter waiter1 = new Waiter("Borus Waiter", _productStorage);
            Waiter waiter2 = new Waiter("Anton Waiter", _productStorage);

            Waiter.OrderDelivered += ConnectorHandler.WaiterDishDeliveredNotify;


            HashSet<Waiter> waiters = new HashSet<Waiter>();
            waiters.Add(waiter1);
            waiters.Add(waiter2);


            _manager = new Manager(_menu);

            _pizzeriaData = new PizzeriaData(waiters, staff, _manager, _chefManager, loyaltyProgram, _menu, cashRegisters, _productStorage);

            Chef.UpdateIngredient += _pizzeriaData.RevisionStorage;

            _admin = new Admin(_pizzeriaData);

            Pizzeria pizzeria = new Pizzeria(_pizzeriaData, _admin);

            _simulator = new Simulator(customerGenerator, pizzeria);

            Console.WriteLine("Hello, World! This is Pizza Project");

        }

        // { "Name": "NameChef", "Ids":[1,2,3,4]}
        // public Chef(string name, Storage storage, Dictionary<string, Recipe> recipes = null)
        //public void SetChefInAdmin(string income, int[] ids)
        //{
        //    string name = income;

        //    List <IOffer> offers = _menu.OffersById(ids);

        //    List<Recipe> result = offers.Select(x => x.Recipe).ToList();

        //    Chef current = new Chef(name, _productStorage, result);

        //    //current.DishPrepared += PrintChefNotify;
        //    current.DishPrepared += ConnectorHandler.ChefDishCompletedNotify;

        //    _admin.AddStaff(current);
        //}

        public void SetChefInAdmin(string chefName, int[] categoryId)
        {
            string name = chefName;

            Category[] cat = new Category[categoryId.Length];

            for (int i = 0; i < categoryId.Length; i++)
            {
                cat[i] = (Category)categoryId[i];
            }

            Chef current = new Chef(name, _productStorage, cat);

            current.DishPrepared += ConnectorHandler.ChefDishCompletedNotify;

            _admin.AddStaff(current);
        }


        public void AddClientAdmin(string name, int vipLvl, decimal amount, int paymentCategory)
        {
            HashSet<VipLvl> vipLvls = new HashSet<VipLvl>() { (VipLvl)vipLvl };
            HashSet<Wallet> walets = new HashSet<Wallet>() { new Wallet(amount, (Wallet.PaymentCategory)paymentCategory) };
            Customer customer = new Customer(name, vipLvls, walets);
            _simulator.AddCustomer(customer);
        }

        public void AddClient(string name, decimal amount, int paymentCategory, int[] dishesId)
        {
            HashSet<Wallet> walets = new HashSet<Wallet>() { new Wallet(amount, (Wallet.PaymentCategory)paymentCategory) };
            Customer customer = new Customer(name, walets, dishesId.ToList());
            _simulator.AddCustomer(customer);
        }

        public void AddWaiter(string name)
        {
            Waiter waiter = new Waiter(name, _productStorage);
            _pizzeriaData.AddWaiter(waiter);
        }

        //Dictionary<IOffer, decimal> dish
        //public void AddDishInMenu(string name, string description, int[] ingredientsId, uint[] ingredientsNumber, uint timePrepare, int size, int dough, decimal price)
        //{
        //    List<Ingredient> fillerIngredient = Filler.GetIngredients();

        //    Dictionary<Ingredient, uint> currentIngr = new Dictionary<Ingredient, uint>();

        //    for (int i = 0; i < ingredientsId.Length; i++)
        //    {
        //        currentIngr.Add(fillerIngredient[ingredientsId[i]], ingredientsNumber[i]);
        //    }

        //    Recipe currentRecipe = new Recipe(name, currentIngr, timePrepare);

        //    IOffer current = new Pizza(name, description, currentRecipe, (Pizza.PizzaDough)dough, (IOffer.Size)size);

        //    Dictionary<IOffer, decimal> dish = new Dictionary<IOffer, decimal>();
        //    dish.Add(current, price);

        //    _manager.AddDish(dish);

        //    _chefManager.Chefs.ForEach(x => x.Recipes.Add(currentRecipe));
        //}


        public void AddDishInMenu(string name, string description, int[] ingredientsId, uint[] ingredientsNumber, uint timePrepare, int size, int dough, decimal price)
        {
            List<Ingredient> fillerIngredient = Filler.GetIngredients();

            Dictionary<Ingredient, uint> currentIngr = new Dictionary<Ingredient, uint>();

            for (int i = 0; i < ingredientsId.Length; i++)
            {
                currentIngr.Add(fillerIngredient[ingredientsId[i]], ingredientsNumber[i]);
            }

            Recipe currentRecipe = new Recipe(name, currentIngr, timePrepare);

            IOffer current = new Pizza(name, description, currentRecipe, (Pizza.PizzaDough)dough, (IOffer.Size)size);

            Dictionary<IOffer, decimal> dish = new Dictionary<IOffer, decimal>();
            dish.Add(current, price);

            _manager.AddDish(dish);

            _chefManager.Chefs.ForEach(x => x.Categories.Add(current.Category));
        }

        public string GetCashRegister()
        {
            HashSet<ICashRegister> cashRegs = _pizzeriaData.CashRegs;

            var res = cashRegs.Select(x => new { PaymentMethod = x.PaymentMethod, Queue = x.CustomersInQueue }).ToList();

            string result = JsonConvert.SerializeObject(res);
            return result;
        }

        public string GetMenu()
        {
            //string result = JsonConvert.SerializeObject( _menu );
            Dictionary<IOffer, decimal> dishes = _menu.Dishes;

            var res = dishes.Select(x => new { Dish = x.Key, Price = x.Value }).ToList();

            string result = JsonConvert.SerializeObject(res);
            return result;
        }

        public string GetIngredients()
        {
            string result = JsonConvert.SerializeObject(Filler.GetIngredients());
            return result;
        }

        public void SetConnector(IConnector connector)
        {
            ConnectorHandler.SetConnector(connector);
        }

    }
}