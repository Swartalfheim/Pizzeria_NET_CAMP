using PizzaProject.Administration;
using PizzaProject.Costumer_Payment.People;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Menu_Loyality.Loyality
{
    public class LoyaltyProgram
    {
        private Dictionary<Customer, HashSet<VipLvl>> _customers = new Dictionary<Customer, HashSet<VipLvl>>();

        internal Dictionary<Customer, HashSet<VipLvl>> Customers { get => new Dictionary<Customer, HashSet<VipLvl>>(_customers); set => _customers = value; }
        internal HashSet<VipLvl> VipStatus { get => _vipStatus; private set => _vipStatus = value; }

        private HashSet<VipLvl> _vipStatus = new HashSet<VipLvl>();

        public void AddCustomer(Customer customer)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));
            _customers.Add(customer, new HashSet<VipLvl>());
        }

        public void SetCustomerStatus(Customer customer, HashSet<VipLvl> status)
        {
            if (status is null) throw new ArgumentNullException(nameof(status));
            _customers[customer] = status;
            _vipStatus = status;
        }
    }
}
