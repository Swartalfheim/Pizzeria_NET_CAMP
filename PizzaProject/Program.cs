using PizzeriaTask_my_part_;
namespace PizzaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            

            var CustomerGenerator = new CustomerGenerator(5, new List<uint>(), new RandomCustomerGenerator(0, 15));
            var users = CustomerGenerator.GenerateVisitorsForSimulation();

            foreach (var user in users)
            {
                Console.WriteLine(user.Name + user.VipLvls);
            }

            Console.WriteLine("Hello, World!");
        }
    }
}