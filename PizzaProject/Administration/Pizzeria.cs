using PizzaProject.Costumer_Payment.CashRegisters;

namespace PizzaProject.Administration
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
