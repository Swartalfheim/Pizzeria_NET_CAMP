using PizzaProject.Administration;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.CustomersGeneration;

namespace PizzaProject.Simulation
{
    internal class Simulator
    {
        private CustomerGenerator _customerGenerator;
        private List<Customer> _customers;
        private Pizzeria _pizzeria;

        //private IVisualization _visualization;

        public Simulator(CustomerGenerator customerGenerator, Pizzeria pizzeria)//, IVisualization visualization)
        {
            if (customerGenerator == null || pizzeria == null)// || visualization == null)
            {
                throw new ArgumentException("Incorrect data");
            }

            _customerGenerator = customerGenerator;
            _pizzeria = pizzeria;
            //_visualization = visualization;
            _customers = _customerGenerator.GenerateVisitorsForSimulation();
        }

        public void StartSimulation()
        {
            var cashReg = _pizzeria.CashRegs;
            foreach (var item in cashReg)
            {
                item.NewOrderApperiance += _pizzeria.PizzeriaData.ChefManager.AddOrder;
            }
            _customers.ForEach(c => c.GetInLine(_pizzeria.CashRegs));


        }
        public void StopSimulation()
        {

        }

        /*public void SetCustomerGenerator()
        {

        }*/
    }
}
