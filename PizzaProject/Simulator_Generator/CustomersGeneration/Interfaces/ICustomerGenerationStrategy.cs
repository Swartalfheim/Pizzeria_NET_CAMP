using PizzaProject.Costumer_Payment.People;

namespace PizzaProject.CustomersGeneration.Interfaces
{
    public interface ICustomerGenerationStrategy
    {
        List<Customer> GenerateVisitors();
    }
}
