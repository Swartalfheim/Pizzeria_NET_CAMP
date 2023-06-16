using PizzaProject.Costumer_Payment;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.CustomersGeneration.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.CustomersGeneration
{
    internal class RandomCustomerGenerator : ICustomerGenerationStrategy
    {
        private int _minAmountOfCustomers = 0;
        private int _maxAmountOfCustomers = 100;

        public RandomCustomerGenerator(int minAmountOfCustomers, int maxAmountOfCustomers)
        {
            if(minAmountOfCustomers < 0 || minAmountOfCustomers >= maxAmountOfCustomers)
            {
                throw new ArgumentOutOfRangeException("Invalid range for generation");
            }
            _minAmountOfCustomers = minAmountOfCustomers;
            _maxAmountOfCustomers = maxAmountOfCustomers;
        }

        public List<Customer> GenerateVisitors()
        {
            Random r = new Random();
            List<Customer> customers = new List<Customer>(r.Next(_minAmountOfCustomers, _maxAmountOfCustomers));
            for (int i = 0; i < customers.Capacity; i++)
            {
                string userName = Guid.NewGuid().ToString();

                //VipLvls
                HashSet<VipLvl> vipLvls = CustomerGenerator.GenerateVipLvls(r.Next(1, 3), Enum.GetValues(typeof(VipLvl)));

                //Wallets
                HashSet<Wallet> wallets = CustomerGenerator.GenerateWallets(r.Next(0, 9), Enum.GetValues(typeof(Wallet.PaymentCategory)));

                Customer customer = new Customer(userName, vipLvls, wallets);
                customers.Add(customer);
            }
            return customers;
        }
    }
}
