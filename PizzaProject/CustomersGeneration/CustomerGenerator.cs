using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTask_my_part_.TemporaryClasses;

namespace PizzeriaTask_my_part_
{
    internal class CustomerGenerator
    {
        private uint _customerCount;
        //private List<uint> _intervals;
        private ICustomerGenerationStrategy _customerGenerationStrategy;

        public CustomerGenerator(uint customerCount, List<uint> intervals, ICustomerGenerationStrategy customerGenerationStrategy)
        {
            _customerCount = customerCount;
            //_intervals = new List<uint>(intervals);
            _customerGenerationStrategy = customerGenerationStrategy;
        }

        public List<Customer> GenerateVisitorsForSimulation()
        {
            return _customerGenerationStrategy.GenerateVisitors();
        }
    }
}
