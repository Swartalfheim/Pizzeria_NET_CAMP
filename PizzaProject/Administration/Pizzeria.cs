using PizzaProject.Costumer_Payment.CashRegisters;

namespace PizzaProject.Administration
{
    public class Pizzeria
    {
        private PizzeriaData _pizzeriaData;
        private Admin _admin;


        public PizzeriaData PizzeriaData 
        {
            get { return _pizzeriaData; }
        }

        public Pizzeria(PizzeriaData pizzeriaData, Admin admin)
        {
            _pizzeriaData = pizzeriaData;
            _admin = admin;
        }

        //public IEnumerable<ICashRegister> CashRegs { get => _pizzeriaData.CashRegs; }
        public HashSet<ICashRegister> CashRegs { get => _pizzeriaData.CashRegs; }
        public Storage Storage { get => _pizzeriaData.ProductStorage; }

    }
}
