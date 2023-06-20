using PizzaProject.Costumer_Payment;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.CustomersGeneration;
using PizzaProject.Simulator_Generator.CustomersGeneration.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Simulator_Generator.CustomersGeneration
{
    internal class DefaultCustomerGenerator : ICustomerGenerationStrategy
    {
        private uint _customerCount = 1;

        public DefaultCustomerGenerator()
        {
        }
        public DefaultCustomerGenerator(uint amountOfCustomers)
        {
            _customerCount = amountOfCustomers;
        }

        public List<Customer> GenerateVisitors()
        {
            Random r = new Random();
            int customerNumber = r.Next(1, 11);
            List<Customer> customers = new List<Customer>();
            for (uint i = 0; i < customerNumber; i++)
            {
                string userName = RandomNameGenerator.GenerateRandomName();

                //VipLvls
                HashSet<VipLvl> vipLvls = CustomerGenerator.GenerateVipLvls(r.Next(1, Enum.GetValues(typeof(VipLvl)).Length), Enum.GetValues(typeof(VipLvl)));

                //Wallets
                HashSet<Wallet> wallets = CustomerGenerator.GenerateWallets(r.Next(1, Enum.GetValues(typeof(Wallet.PaymentCategory)).Length), Enum.GetValues(typeof(Wallet.PaymentCategory)));

                Customer customer = new Customer(userName, vipLvls, wallets);
                customers.Add(customer);
            }
            return customers;
        }
    }

    public static class RandomNameGenerator
    {
        private static Random random = new Random();
        private static List<string> possibleNames = new List<string>
        {
            "John", "Emily", "Michael", "Sophia", "Robert", "Emma", "Daniel",
            "Olivia", "William", "Ava", "James", "Isabella", "Benjamin", "Mia",
            "Joseph", "Charlotte", "David", "Amelia", "Andrew", "Harper",
            "Matthew", "Evelyn", "Jacob", "Abigail", "Logan", "Emily", "Alexander",
            "Elizabeth", "Ethan", "Mila", "Liam", "Sofia", "Noah", "Avery",
            "Lucas", "Grace", "Mason", "Chloe", "Elijah", "Ella", "Henry", "Zoe",
            "Sebastian", "Scarlett", "Jackson", "Lily", "Aiden", "Madison"
        };

        public static string GenerateRandomName()
        {
            int index = random.Next(possibleNames.Count);
            return possibleNames[index];
        }
    }
}