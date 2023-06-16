using PizzaProject;

namespace PizzaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customerGenerator = new CustomerGenerator(new DefaultCustomerGenerator(10));
            //var CustomerGenerator = new CustomerGenerator(new RandomCustomerGenerator(0, 15));
            var users = customerGenerator.GenerateVisitorsForSimulation();

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
                foreach (var lvl in user.VipLvls)
                {
                    System.Console.WriteLine(lvl);
                }
                System.Console.WriteLine();
            }

            Console.WriteLine("Hello, World!");
        }
    }
}