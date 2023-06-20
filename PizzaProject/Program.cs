using PizzaProject.Administration;
using PizzaProject.Costumer_Payment;
using PizzaProject.Costumer_Payment.CashRegisters;
using PizzaProject.CustomersGeneration;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Menu_Loyality.Loyality;
using PizzaProject.Menu_Loyality.Menu;
using PizzaProject.Simulation;
using PizzaProject.Simulator_Generator.CustomersGeneration;
using PizzaProject.Storage_Waiter.Interfaces;
using PizzaProject.Storage_Waiter.Staff;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customerGenerator = new CustomerGenerator(new DefaultCustomerGenerator(10));
            //var CustomerGenerator = new CustomerGenerator(new RandomCustomerGenerator(0, 15));
            //var users = customerGenerator.GenerateVisitorsForSimulation();




            HashSet<Wallet> wallets = new HashSet<Wallet>();
            wallets.Add(new Wallet(400m, Wallet.PaymentCategory.Card));
            wallets.Add(new Wallet(100m, Wallet.PaymentCategory.Cash));
            wallets.Add(new Wallet(200m, Wallet.PaymentCategory.PaymentService));

            HashSet<ICashRegister> cashRegisters = new HashSet<ICashRegister>();

            Menu menu = Menu.GetInstance();

            cashRegisters.Add(new CashRegister(menu, wallets));
            cashRegisters.Add(new CashRegister(menu, wallets));

            foreach (var item in cashRegisters)
            {
                item.NewOrderApperiance += PrintOrderWaiterNotify;
            }

            Storage productStorage = new Storage(Filler.GetIngredientForStorage());

            LoyaltyProgram loyaltyProgram = new LoyaltyProgram();

            List<Chef> chefs = new List<Chef>
            {
                new Chef("Anton", productStorage, Category.Drinks),
                new Chef("Oleg", productStorage,  Category.Pizzas, Category.Sweets),
                new Chef("Ivan", productStorage,  Category.Pizzas, Category.Sweets)
            };

            
            chefs.ForEach(x => x.DishPrepared += PrintChefNotify);

            HashSet<IStaff> staff = new HashSet<IStaff>();
            staff = chefs.Select(x => (IStaff)x).ToHashSet();

            ChefManager chefManager = new ChefManager("Big Bos", chefs, productStorage);

            Waiter waiter1 = new Waiter("Tony Ferguson", productStorage);
            Waiter waiter2 = new Waiter("Muhammad Ali", productStorage);

            Waiter.OrderDelivered += PrintWaiterNotify;
            

            HashSet<Waiter> waiters = new HashSet<Waiter>();
            waiters.Add(waiter1);
            waiters.Add(waiter2);


            Manager manager = new Manager(menu);

            PizzeriaData pizzeriaData = new PizzeriaData(waiters, staff, manager, chefManager, loyaltyProgram, menu, cashRegisters, productStorage);

            Chef.UpdateIngredient += pizzeriaData.RevisionStorage;

            Admin admin = new Admin(pizzeriaData);

            

            Pizzeria pizzeria = new Pizzeria(pizzeriaData, admin);

            Simulator simulator = new Simulator(customerGenerator, pizzeria);





            Console.WriteLine("Hello, World!");

            simulator.StartSimulation();

            Console.ReadLine();
        }

        public static void PrintChefNotify(string name, string dishName)
        {
            Console.WriteLine($"Chef {name} has COOKED {dishName}");
        }

        public static void PrintWaiterNotify(string name, Order order)
        {
            Console.WriteLine($"Waiter {name} has DELIVERED {order}");
        }

        public static void PrintOrderWaiterNotify(Order order)
        {
            Console.WriteLine($"\tOrder: {string.Join(" ", order)} Formed");
        }

    }
}