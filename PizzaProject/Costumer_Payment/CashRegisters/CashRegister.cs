using PizzaProject.Costumer_Payment.People;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Menu_Loyality.Menu;

namespace PizzaProject.Costumer_Payment.CashRegisters
{
    public class CashRegister : ICashRegister
    {
        private IMenu _menu;
        public IMenu Menu { get => _menu; }

        HashSet<Wallet> _localWallets;
        public IEnumerable<Wallet.PaymentCategory> PaymentMethod { get => _localWallets.Select(x => x.Category).ToHashSet(); }

        List<Customer> _customersInQueue = new List<Customer>(); 
        public int CustomersInQueue { get => _customersInQueue.Count; }

        private Order _currentOrder;
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

            if (_customersInQueue.Count == 1)
                _customersInQueue[0].MakeOrder(this);
        }

        public void DeleteFromQueue(Customer customer)
        {
            _customersInQueue.Remove(customer);
        }

        public void MakeOrder(Customer customer)
        {
            _currentOrder = new();
            _currentCustomer = customer;
        }

        public bool AddDishesToOrder(IOffer dish, List<Ingredient> additional)
        {
            //перевірити, чи відповідає статус користувача обраній страві
            //if(Menu.SpecialOffer.Contains(dish))
            //_currentCustomer.VipLvl

            // _currentOrder.Add()

            throw new NotImplementedException();
        }

        public bool AddDishesToOrder(uint dishId, int[] additionalIds, bool special = false)
        {
            //в ідеалі передавати пове замовлення

            //перевірити, чи відповідає статус користувача обраній страві
            if (special)
            {

            }
            //if(Menu.SpecialOffer.Contains(dish))
            //_currentCustomer.VipLvl

            // _currentOrder.Add()

            //var dish = menu.Dishes.Keys.Where(offer => offer.Name.Equals("Піцца Гавайська")).FirstOrDefault();
            //var additional = menu.AdditionalIngredients[dish].Where(a => a.Name == "Томати").FirstOrDefault();
            //List<CostsOfIngredients> additionals = new() { additional };
            //cashreg.AddDishToOrder(dish, additionals);
            //cashreg.AddDishToOrder(dish, additionals);
            return true;
        }
        
        public bool Pay(Wallet customerWallet)
        {
            /* if (customerWallet.SendPayment(_currentOrder.TotalPrice, _localWallets.First(lw => lw.Category == customerWallet.Category)))
            {
                NewOrderApperiance(_currentOrder);
                _currentCustomer.SetOrderId(order.Id)
                //оповіщення посередника про зміну кількості грошей на рахунку конкретної каси (_currentOrder.TotalPrice)

                return true;
            }
            return false; */

            throw new NotImplementedException();
        }
    }
}
