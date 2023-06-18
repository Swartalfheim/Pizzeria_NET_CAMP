using PizzaProject.Administration;
using PizzaProject.Costumer_Payment;
using PizzaProject.Costumer_Payment.CashRegisters;
using PizzaProject.CustomersGeneration;
using PizzaProject.Menu_Loyality.Loyality;
using PizzaProject.Menu_Loyality.Menu;
using PizzaProject.Simulation;
using PizzaProject.Simulator_Generator.CustomersGeneration;
using PizzaProject.Storage_Waiter.Interfaces;
using PizzaProject.Storage_Waiter.Staff;

namespace PizzaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customerGenerator = new CustomerGenerator(new DefaultCustomerGenerator(10));
            //var CustomerGenerator = new CustomerGenerator(new RandomCustomerGenerator(0, 15));
            var users = customerGenerator.GenerateVisitorsForSimulation();




            HashSet<Wallet> wallets = new HashSet<Wallet>();
            wallets.Add(new Wallet(400m, Wallet.PaymentCategory.Card));
            wallets.Add(new Wallet(100m, Wallet.PaymentCategory.Cash));
            wallets.Add(new Wallet(200m, Wallet.PaymentCategory.PaymentService));

            HashSet<ICashRegister> cashRegisters = new HashSet<ICashRegister>();

            Menu menu = Menu.GetInstance();

            cashRegisters.Add(new CashRegister(menu, wallets));
            cashRegisters.Add(new CashRegister(menu, wallets));

            Storage productStorage = new Storage(Filler.GetIngredientForStorage());

            LoyaltyProgram loyaltyProgram = new LoyaltyProgram();

            List<Chef> chefs = new List<Chef>
            { 
                new Chef("Anton", productStorage, Filler.GetForFirstChef()),
                new Chef("Oleg", productStorage, Filler.GetForFirstChef()),
                new Chef("Ivan", productStorage, Filler.GetForFirstChef())
            };

            chefs.ForEach(x => x.DishPrepared += Print);

            HashSet<IStaff> staff = new HashSet<IStaff>();
            staff = chefs.Select(x => (IStaff)x).ToHashSet();

            ChefManager chefManager = new ChefManager("Big Bos", chefs);

            Manager manager = new Manager(menu);

            PizzeriaData pizzeriaData = new PizzeriaData(staff, manager, chefManager, loyaltyProgram, menu, cashRegisters, productStorage);


            Admin admin = new Admin(pizzeriaData);

            

            Pizzeria pizzeria = new Pizzeria(pizzeriaData, admin);

            Simulator simulator = new Simulator(customerGenerator, pizzeria);

            //foreach (var user in users)
            //{
            //    Console.WriteLine(user.Name);
            //    foreach (var lvl in user.VipLvls)
            //    {
            //        System.Console.WriteLine(lvl);
            //    }
            //    System.Console.WriteLine();
            //}



            Console.WriteLine("Hello, World!");

            simulator.StartSimulation();

            Console.ReadLine();
        }

        public static void Print(string name, string dishName)
        {
            Console.WriteLine($"Chef {name} has cooked {dishName}");
        }
    }
}