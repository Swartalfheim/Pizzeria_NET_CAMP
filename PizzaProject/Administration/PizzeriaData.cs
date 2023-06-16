using PizzaProject.Costumer_Payment.CashRegisters;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.Menu_Loyality.Loyality;
using PizzaProject.Menu_Loyality.Menu;

namespace PizzaProject.Administration
{
    public class PizzeriaData
    {
        private Manager _manager;
        private HashSet<IStaff> _staff;
        private ChefManager _chefManager;
        private LoyaltyProgram _loyaltyProgram;
        private Menu _menu;
        private HashSet<ICashRegister> _cashRegs;
        private Storage _productStorage;

        public PizzeriaData(HashSet<IStaff> staff, Manager manager, ChefManager chefManager, LoyaltyProgram loyaltyProgram, Menu menu, HashSet<ICashRegister> cashRegs, Storage productStorage)
        {
            _staff = new HashSet<IStaff>();
            foreach (var item in staff)
            {
                AddStaff(item);
            }

            _manager = manager;
            _chefManager = chefManager;

            _loyaltyProgram = loyaltyProgram;
            _menu = menu;

            _cashRegs = new HashSet<ICashRegister>();
            foreach (var item in cashRegs)
            {
                AddICashRegister(item);
            }

            _productStorage = productStorage;
        }

        public Manager Manager { get => _manager; }
        public IEnumerable<IStaff> Staff { get => _staff; }
        public ChefManager ChefManager { get => _chefManager; }
        public LoyaltyProgram LoyaltyProgram { get => _loyaltyProgram; }
        public Menu Menu { get => _menu; }
        public IEnumerable<ICashRegister> CashRegs { get => _cashRegs; }
        public Storage ProductStorage { get => _productStorage; }

        public string GetStaffInfo(IStaff staff)
        {
            return staff.Info;
        }

        public void SetVipStatus(Customer customer, HashSet<VipLvl> status)
        {
            customer.SetVipLvls(status);
        }

        public void AddStaff(IStaff staff)
        {
            if (staff != null && !(staff is Manager) && !(staff is ChefManager))
            {
                _staff.Add(staff);
            }
        }

        public void RemoveStaff(IStaff staff)
        {
            _staff.Remove(staff);
        }

        public void AddICashRegister(ICashRegister cashRegister)
        {
            if (cashRegister != null)
            {
                _cashRegs.Add(cashRegister);
            }
        }

        public void RemoveICashRegister(ICashRegister cashRegister)
        {
            _cashRegs.Remove(cashRegister);
        }

        public void SetManager(IPerson person)
        {
            if (person != null)
            {
                Manager manager = new Manager(person.Id, person.Name, _menu);
                _manager = manager;
            }
        }













        public enum VipLvl
        {
            None,
            Bronze,
            Silver,
            Gold
        }
        public enum Category
        {
            Drinks,
            Pizzas,
            Sweets
        }
    }
}
