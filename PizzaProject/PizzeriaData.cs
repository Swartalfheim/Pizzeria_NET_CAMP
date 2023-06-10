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
            foreach (var obj in args)
            {
                if (obj != null)
                {
                    if (obj is LoyaltyProgram)
                    {
                        _loyaltyProgram = obj as LoyaltyProgram;
                    }
                    else if (obj is Menu)
                    {
                        _menu = obj as Menu;
                    }
                    else if (obj is Storage)
                    {
                        _productStorage = obj as Storage;
                    }
                    else if (obj is Manager)
                    {
                        _manager = obj as Manager;
                    }
                    else if (obj is ChefManager)
                    {
                        _chefManager = obj as ChefManager;
                    }
                    else if (obj is HashSet<IStaff>)
                    {
                        _staff = new HashSet<IStaff>();
                        var tempStaff = obj as HashSet<IStaff>;
                        foreach (var item in tempStaff)
                        {
                            _staff.Add(item);
                        }
                    }
                    else if (obj is HashSet<ICashRegister>)
                    {
                        _cashRegs = new HashSet<ICashRegister>();
                        var tempCashRegs = obj as HashSet<ICashRegister>;
                        foreach (var item in tempCashRegs)
                        {
                            _cashRegs.Add(item);
                        }
                    }
                }
            }
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
            customer.VipStatus.RemoveWhere(_ => true);
            foreach (var vipLvl in status)
            {
                customer.VipStatus.Add(vipLvl);
            }
        }

        public void AddStaff(IStaff staff)
        {
            _staff.Add(staff);
        }

        public void RemoveStaff(IStaff staff)
        {
            _staff.Remove(staff);
        }

        public void AddICashRegister(ICashRegister cashRegister)
        {
            _cashRegs.Add(cashRegister);
        }

        public void RemoveICashRegister(ICashRegister cashRegister)
        {
            _cashRegs.Remove(cashRegister);
        }

        public void SetManager(IPerson person)
        {
            Manager manager = new Manager(person.Id, person.Name, _menu);
            _manager = manager;
        }
    }
}
