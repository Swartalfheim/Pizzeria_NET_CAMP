using PizzaProject.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject
{
    internal class PizzeriaData
    {
        private Manager _manager;
        private HashSet<IStaff> _staff;
        private ChefManager _chefManager;
        private LoyaltyProgram _loyaltyProgram;
        private Menu _menu;
        private HashSet<ICashRegister> _cashRegs;
        private Storage _productStorage;

        public PizzeriaData(object[] args)
        {

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
            return "";
        }

        public void SetVipStatus(Customer customer, HashSet<VipLvl> status)
        {

        }

        public void AddStaff(IStaff staff)
        {

        }

        public void RemoveStaff(IStaff staff)
        {

        }

        public void AddICashRegister(ICashRegister cashRegister)
        {

        }

        public void RemoveICashRegister(ICashRegister cashRegister)
        {

        }

        public void SetManager(IPerson person)
        {

        }
    }
}
