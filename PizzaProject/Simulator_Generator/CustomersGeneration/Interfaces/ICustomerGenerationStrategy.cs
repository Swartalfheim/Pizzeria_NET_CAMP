using PizzaProject.Costumer_Payment.People;

namespace PizzaProject.Simulator_Generator.CustomersGeneration.Interfaces
{
    public interface ICustomerGenerationStrategy
    {
        List<Customer> GenerateVisitors();
    }
}
