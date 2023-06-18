using PizzaProject.Costumer_Payment.CashRegisters;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Costumer_Payment.People
{
    public class Customer : IPerson
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private HashSet<VipLvl> _vipLvls;
        public IEnumerable<VipLvl> VipLvls => _vipLvls;

        private HashSet<Wallet> _wallets;

        private Guid _currentOrderId;

        public Customer(string name, HashSet<Wallet> wallets)
        {
            Id = Guid.NewGuid();
            Name = name;
            _vipLvls = new HashSet<VipLvl>() { VipLvl.None };
            _wallets = new HashSet<Wallet>(wallets);
        }

        public Customer(string name, HashSet<VipLvl> vipLvls, HashSet<Wallet> wallets)
        {
            Id = Guid.NewGuid();
            Name = name;
            _vipLvls = new HashSet<VipLvl>(vipLvls);
            _wallets = new HashSet<Wallet>(wallets);
        }

        public void SetVipLvls(HashSet<VipLvl> vipLvls)
        {
            _vipLvls = new HashSet<VipLvl>(vipLvls);
        }

        public void SetOrderId(Guid orderId)
        {
            _currentOrderId = orderId;
        }

        public void GetInLine(HashSet<ICashRegister> cashRegs) //pizzeria -> ICashRegisters
        {
            var cashreg = cashRegs.Where(c => c.CustomersInQueue == cashRegs.Min(c => c.CustomersInQueue)).FirstOrDefault();
            cashreg.AddToQueue(this);
        }

        public void MakeOrder(ICashRegister cashreg) 
        {
            var menu = cashreg.Menu; //dish, additional, price = menu item

            //!!! порядковий номер страв, колекція порядк номерів дод. інгр
            
            FormOrder(cashreg);
            //локальний кошик з номерів страв 
            //SelectDishes
            // 1, (2,3)
            //2
            //3, (1)

            //Pay(cashreg, Wallet.PaymentCategory.Card); //pref - з тих, що є
            Pay(cashreg, _wallets.First().Category);
        }

        private void FormOrder(ICashRegister cashreg)
        {
            Random random = new Random();
            int countDish = random.Next(1, cashreg.Menu.Dishes.Count() + 1);
            int[] dishes = new int[countDish];
            for (int i = 0; i < dishes.Length; i++)
            {
                dishes[i] = random.Next(0, cashreg.Menu.Dishes.Count());
            }

            cashreg.AddDishesToOrder(dishes);

            //Thread.Sleap(rand)
            //SelectDish
        }

        public void SelectDish(uint dishId, int[] additionalIds)
        {
            //cashReg.AddDish()
        }

        private bool Pay(ICashRegister cashReg, Wallet.PaymentCategory prefferdWayToPay)
        {
            Wallet wayToPay = _wallets.Where(w => w.Category == prefferdWayToPay).First();

            if (cashReg.PaymentMethod.Contains(wayToPay.Category))
            {
                if (cashReg.Pay(wayToPay))
                    return true;
            }
            else
            {
                foreach (var wallet in _wallets.Except(new HashSet<Wallet>() { wayToPay }))
                {
                    if (cashReg.PaymentMethod.Contains(wallet.Category))
                    {
                        if (cashReg.Pay(wallet))
                            return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} (VipLvls: {string.Join(", ", _vipLvls)})";
        }
    }
}
