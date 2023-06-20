using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PizzaProject.Costumer_Payment.CashRegisters;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Costumer_Payment.People
{
    public class Customer : IPerson
    {
        //public Guid Id { get; private set; }
        public uint Id { get; private set; }
        public string Name { get; private set; }


        private HashSet<VipLvl> _vipLvls;

        [JsonProperty("VipLvls")]
        public IEnumerable<VipLvl> VipLvls => _vipLvls;

        [JsonProperty("Wallets")]
        private HashSet<Wallet> _wallets;

        [JsonProperty("CurrentOrderId")]
        private uint _currentOrderId;

        private List<int> _customerOrder = new List<int>();

        public Customer(string name, HashSet<Wallet> wallets, List<int> customerOrder = null)
        {
            //Id = Guid.NewGuid();
            Id = UniqueIntGenerator.GetUniqueCustomerInt();
            Name = name;
            _vipLvls = new HashSet<VipLvl>() { VipLvl.None };
            _wallets = new HashSet<Wallet>(wallets);
            if (customerOrder != null)
            {
                _customerOrder = customerOrder;
            }
        }

        public Customer(string name, HashSet<VipLvl> vipLvls, HashSet<Wallet> wallets)
        {
            //Id = Guid.NewGuid();
            Id = UniqueIntGenerator.GetUniqueCustomerInt();
            Name = name;
            _vipLvls = new HashSet<VipLvl>(vipLvls);
            _wallets = new HashSet<Wallet>(wallets);
        }

        public void SetVipLvls(HashSet<VipLvl> vipLvls)
        {
            _vipLvls = new HashSet<VipLvl>(vipLvls);
        }

        public void SetOrderId(uint orderId)
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
            List<int> dishes = new List<int>();
            if (_customerOrder.Count == 0)
            {
                dishes = GenerateOrder(cashreg);
            }
            else
            {
                dishes = _customerOrder;
            }
            
            cashreg.AddDishesToOrder(dishes.ToArray());
            Pay(cashreg, _wallets.First().Category);
        }

        private List<int> GenerateOrder(ICashRegister cashreg)
        {
            Random random = new Random();
            int countDish = random.Next(1, cashreg.Menu.Dishes.Count() + 1);
            int[] dishes = new int[countDish];
            for (int i = 0; i < dishes.Length; i++)
            {
                dishes[i] = random.Next(0, cashreg.Menu.Dishes.Count());
            }
            return new List<int>(dishes);
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
