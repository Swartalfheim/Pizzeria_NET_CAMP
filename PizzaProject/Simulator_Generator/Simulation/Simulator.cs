using PizzaProject.Administration;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.CustomersGeneration;

namespace PizzaProject.Simulation
{
    public class Simulator
    {
        private CustomerGenerator _customerGenerator;
        private List<Customer> _customers = new List<Customer>();
        private Pizzeria _pizzeria;
        private bool _stopRequest = true;
        private Random _random = new Random();

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
            
        }

        public void StartSimulation()
        {
            //_customers = _customerGenerator.GenerateVisitorsForSimulation();

            Task.Run(() => GenerateCust());

            _pizzeria.PizzeriaData.StartWaiter();
            var cashReg = _pizzeria.CashRegs;
            foreach (var item in cashReg)
            {
                item.NewOrderApperiance += _pizzeria.PizzeriaData.ChefManager.AddOrder;
            }
            //_customers.ForEach(c => c.GetInLine(_pizzeria.CashRegs));
            _pizzeria.PizzeriaData.ChefManager.Chefs.ForEach(x => ConnectorHandler.SendChefData(x));
        }

        public void ContinueSimulation()
        {
            _stopRequest = true;
            Task.Run(() => GenerateCust());
        }

        public void StopSimulation()
        {
            _stopRequest = false;
        }

        private void GenerateCust()
        {
            while (_stopRequest)
            {
                List<Customer> currCustomers = _customerGenerator.GenerateVisitorsForSimulation();
                currCustomers.ForEach(c => c.GetInLine(_pizzeria.CashRegs));
                _customers.AddRange(currCustomers);

                Thread.Sleep(_random.Next(5000, 15000));
            }
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            ConnectorHandler.NewClientAddNotify(customer);
            customer.GetInLine(_pizzeria.CashRegs);
        }
    }
}
