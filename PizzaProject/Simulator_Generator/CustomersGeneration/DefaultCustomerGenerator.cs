namespace PizzaProject.CustomersGeneration
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
            List<Customer> customers = new List<Customer>((int)_customerCount);
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