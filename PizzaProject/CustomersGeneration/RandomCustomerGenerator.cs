using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTask_my_part_.TemporaryClasses;

namespace PizzeriaTask_my_part_
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

                //Wallets
                HashSet<Wallet> wallets = GenerateWallets(r.Next(0, 9), Enum.GetValues(typeof(Wallet.PaymentCategory)));

                Customer customer = new Customer(userName, wallets);
                customers.Add(customer);
            }
            return customers;
        }

        public HashSet<Wallet> GenerateWallets(int amountOfWallets, Array paymentCategories)
        {
            Random random = new Random();
            try
            {
                HashSet<Wallet> wallets = new HashSet<Wallet>(amountOfWallets);

                Wallet.PaymentCategory randomCategory = (Wallet.PaymentCategory)paymentCategories.GetValue(random.Next(paymentCategories.Length));

                for (int i = 0; i < amountOfWallets; i++)
                {
                    Wallet wallet = new Wallet(new decimal(random.NextDouble() * (100000 - 200) + 200), randomCategory);
                    wallets.Add(wallet);
                }
                return wallets;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
