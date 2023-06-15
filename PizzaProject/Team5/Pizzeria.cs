using PizzaProject.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject
{
    internal class Pizzeria
    {
        private PizzeriaData _pizzeriaData;
        private Admin _admin;

        public Pizzeria(PizzeriaData pizzeriaData, Admin admin)
        {
            _pizzeriaData = pizzeriaData;
            _admin = admin;
        }

        public IEnumerable<ICashRegister> CashRegs { get => _pizzeriaData.CashRegs; }
    }
}
