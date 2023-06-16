using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTask_my_part_.TemporaryClasses;
using PizzeriaTask_my_part_.TemporaryClasses.Interfaces;

namespace PizzeriaTask_my_part_
{
    internal class Simulator
    {
        private CustomerGenerator _customerGenerator;
        private List<Customer> _customers;
        private Pizzeria _pizzeria;
        private IVisualization _visualization;

        public Simulator(CustomerGenerator customerGenerator, Pizzeria pizzeria, IVisualization visualization)
        {
            if (customerGenerator == null || pizzeria == null || visualization == null)
            {
                throw new ArgumentException("Incorrect data");
            }

            _customerGenerator = customerGenerator;
            _pizzeria = pizzeria;
            _visualization = visualization;
            _customers = _customerGenerator.GenerateVisitorsForSimulation();
        }

        public void StartSimulation()
        {

        }
        public void StopSimulation()
        {

        }

        /*public void SetCustomerGenerator()
        {

        }*/
    }
}
