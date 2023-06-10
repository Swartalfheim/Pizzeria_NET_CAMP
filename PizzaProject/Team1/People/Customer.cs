using PizzaProject.Team1.CashRegisters;
using PizzaProject.Team1.TemporaryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PizzaProject.Team1.People
{
    class Customer : IPerson
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private HashSet<VipLvl> _vipLvls;
        public IEnumerable<VipLvl> VipLvls => _vipLvls;

        private HashSet<Wallet> _wallets;

        private uint _currentOrderId; //Каса передає?

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

        //public void GenerateCustomer() //CustomerGenerator
        //{
        //Name

        //VipLvls

        //Wallets
        //Random rand = new Random();
        //var min = Enum.GetValues(typeof(Wallet.PaymentCategory)).Cast<int>().Min();
        //var max = Enum.GetValues(typeof(Wallet.PaymentCategory)).Cast<int>().Max();

        //HashSet<Wallet> wallets = new HashSet<Wallet>() { new Wallet(new decimal(rand.NextDouble() * (100000 - 200) + 200), (Wallet.MoneyCategory)rand.Next(min, max));
        //}

        public void SetVipLvls(HashSet<VipLvl> vipLvls)
        {
            _vipLvls = new HashSet<VipLvl>(vipLvls);
        }
        public void MakeOrder(HashSet<ICashRegister> cashRegs) //pizzeria -> ICashRegisters
        {
            //вибір cashReg за меншою кількістю людей у черзі
            var cashreg = cashRegs.Where(c => c.CustomersInQueue == cashRegs.Min(c => c.CustomersInQueue)).FirstOrDefault();

            cashreg.MakeOrder(this); //якщо з черги, то можна і без цього

            var menu = cashreg.Menu; //dish, additional, price = menu item

            //передача страв рядками? 

            //!!! Category - загальні додаткові інгредієнти, але є і унікальні 

            //var dish = menu.Dishes.Where(d => d.Name.Equals("Піцца Гавайська")); 
            // чи 
            //var dish = menu.GetDish("Піцца Гавайська");
            // чи
            //menu.MenuItems.Where(...)

            //var dishAdditional = menu.GetAdditional(dish);
            //var selectedAdditional.Add(dishAdditional.Where(d => d.Name.Equals("Томати").FirstOrDegault()));

            //cashreg.AddDishToOrder(dish, selectedAdditional);
            //cashreg.AddDishToOrder(dish, selectedAdditional);
            //cashreg.AddDishToOrder(dish, selectedAdditional);

            Pay(cashreg, Wallet.PaymentCategory.Card); //pref - з тих, що є
        }

        public void GenerateOrder() //
        {

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
