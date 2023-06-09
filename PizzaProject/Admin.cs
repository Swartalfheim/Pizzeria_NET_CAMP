using PizzaProject.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject
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
