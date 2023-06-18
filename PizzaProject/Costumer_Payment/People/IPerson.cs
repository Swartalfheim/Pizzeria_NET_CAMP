using System;

namespace PizzaProject.Costumer_Payment.People
{
    public interface IPerson 
    {
        Guid Id { get; }
        string Name { get; }
    }
}
