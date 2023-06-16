namespace PizzeriaTask_my_part_.TemporaryClasses
{
    public class Customer
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

    }
}