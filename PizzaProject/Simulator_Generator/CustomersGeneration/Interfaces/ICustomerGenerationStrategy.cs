namespace PizzaProject.CustomersGeneration.Interfaces
{
    public interface ICustomerGenerationStrategy
    {
        List<Customer> GenerateVisitors();
    }
}
