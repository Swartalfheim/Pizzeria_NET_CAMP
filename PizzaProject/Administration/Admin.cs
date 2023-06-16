using PizzaProject.Costumer_Payment.CashRegisters;
using PizzaProject.Costumer_Payment.People;
namespace PizzaProject.Administration
{
    internal class Admin
    {
        private PizzeriaData _pizzeriaData;

        public Admin(PizzeriaData pizzeriaData)
        {
            _pizzeriaData = pizzeriaData;
        }

        public string GetStaffInfo(IStaff staff)
        {
            return _pizzeriaData.GetStaffInfo(staff);
        }

        public void SetVipStatus(Customer customer, HashSet<PizzeriaData.VipLvl> status)
        {
            _pizzeriaData.SetVipStatus(customer, status);
        }

        public void AddStaff(IStaff staff)
        {
            _pizzeriaData.AddStaff(staff);
        }

        public void RemoveStaff(IStaff staff)
        {
            _pizzeriaData.RemoveStaff(staff);
        }

        public void AddICashRegister(ICashRegister cashRegister)
        {
            _pizzeriaData.AddICashRegister(cashRegister);
        }

        public void RemoveICashRegister(ICashRegister cashRegister)
        {
            _pizzeriaData.RemoveICashRegister(cashRegister);
        }

        public void SetManager(IPerson person)
        {
            _pizzeriaData.SetManager(person);
        }
    }
}
