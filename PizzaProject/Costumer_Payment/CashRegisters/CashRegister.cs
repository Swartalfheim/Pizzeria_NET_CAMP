using PizzaProject.Costumer_Payment.People;
using PizzaProject.Menu_Loyality.Menu;

namespace PizzaProject.Costumer_Payment.CashRegisters
{
    internal class CashRegister : ICashRegister
    {
        private IMenu _menu;
        public IMenu GetMenu { get => _menu; }

        HashSet<Wallet> _localWallets;
        public IEnumerable<Wallet.PaymentCategory> PaymentMethod { get => _localWallets.Select(x => x.Category).ToHashSet(); }

        List<Customer> _customersInQueue = new List<Customer>(); //list чи інша колекція?
        public int CustomersInQueue { get => _customersInQueue.Count; }

        private object _currentOrder; //Order
        private Customer _currentCustomer;

        public event OrderApperiance NewOrderApperiance;

        public CashRegister(IMenu menu, HashSet<Wallet> wallets)
        {
            _menu = menu;
            _localWallets = new HashSet<Wallet>(wallets);
        }

        public void AddToQueue(Customer customer)
        {
            _customersInQueue.Add(customer);
        }
        public void DeleteFromQueue(Customer customer)
        {
            _customersInQueue.Remove(customer);
        }

        public void MakeOrder(Customer customer)
        {
            _currentOrder = new();
            _currentCustomer = customer;
            throw new NotImplementedException();
        }

        public bool AddDishToOrder(IOffer dish, List<Ingredient> additional)
        {
            //перевірити, чи відповідає статус користувача обраній страві
            //_currentOrder.Add(...)

            throw new NotImplementedException();
        }

        public bool Pay(Wallet customerWallet)
        {
            //! _currentOrder.TotalPrice
            /* if (customerWallet.SendPayment(_currentOrder.TotalPrice, _localWallets.First(lw => lw.Category == customerWallet.Category)))
            {
                NewOrderApperiance(_currentOrder);
                return true;
            }
            return false; */

            throw new NotImplementedException();
        }
    }
}
