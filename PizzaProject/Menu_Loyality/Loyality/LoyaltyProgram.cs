using PizzaProject.Administration;
using PizzaProject.Costumer_Payment.People;

namespace PizzaProject.Menu_Loyality.Loyality
{
    internal class LoyaltyProgram
    {
        private Dictionary<Customer, HashSet<PizzeriaData.VipLvl>> _customers = new Dictionary<Customer, HashSet<PizzeriaData.VipLvl>>();

        internal Dictionary<Customer, HashSet<PizzeriaData.VipLvl>> Customers { get => new Dictionary<Customer, HashSet<PizzeriaData.VipLvl>>(_customers); set => _customers = value; }
        internal HashSet<PizzeriaData.VipLvl> VipStatus { get => _vipStatus; private set => _vipStatus = value; }

        private HashSet<PizzeriaData.VipLvl> _vipStatus = new HashSet<PizzeriaData.VipLvl>();

        public void AddCustomer(Customer customer)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));
            _customers.Add(customer, new HashSet<PizzeriaData.VipLvl>());
        }

        public void SetCustomerStatus(Customer customer, HashSet<PizzeriaData.VipLvl> status)
        {
            if (status is null) throw new ArgumentNullException(nameof(status));
            _customers[customer] = status;
            _vipStatus = status;
        }
    }
}
