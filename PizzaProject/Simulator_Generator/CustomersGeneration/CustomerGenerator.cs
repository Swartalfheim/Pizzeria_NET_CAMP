using PizzaProject.Costumer_Payment;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.Simulator_Generator.CustomersGeneration.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.CustomersGeneration
{
    internal class CustomerGenerator
    {
        private ICustomerGenerationStrategy _customerGenerationStrategy;


        public CustomerGenerator(ICustomerGenerationStrategy customerGenerationStrategy)
        {
             _customerGenerationStrategy = customerGenerationStrategy;
        }



        public List<Customer> GenerateVisitorsForSimulation()
        {

            return _customerGenerationStrategy.GenerateVisitors();

        }

        public static HashSet<Wallet> GenerateWallets(int amountOfWallets, Array paymentCategories, int minMoney = 200, int maxMoney = 100000)
        {
            Random random = new Random();
            
            try
            {
                HashSet<Wallet> wallets = new HashSet<Wallet>(amountOfWallets);

                Wallet.PaymentCategory randomCategory = (Wallet.PaymentCategory)paymentCategories.GetValue(random.Next(paymentCategories.Length));

                for (int i = 0; i < amountOfWallets; i++)
                {
                    Wallet wallet = new Wallet(new decimal(random.NextDouble() * (maxMoney - minMoney) + minMoney), randomCategory);
                    wallets.Add(wallet);
                }
                return wallets;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static HashSet<VipLvl> GenerateVipLvls(int amountOflvls, Array VipLvlCategories)
        {
            Random random = new Random();
            try
            {
                HashSet<VipLvl> vipLvls = new HashSet<VipLvl>(amountOflvls);

                for (int i = 0; i < amountOflvls; i++)
                {
                    VipLvl randomVipLvl = (VipLvl)VipLvlCategories.GetValue(random.Next(VipLvlCategories.Length));
                    vipLvls.Add(randomVipLvl);
                }
                return vipLvls;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
