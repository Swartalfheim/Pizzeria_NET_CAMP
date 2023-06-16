using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTask_my_part_.TemporaryClasses;

namespace PizzeriaTask_my_part_
{
    public interface ICustomerGenerationStrategy
    {
        List<Customer> GenerateVisitors();
    }
}
