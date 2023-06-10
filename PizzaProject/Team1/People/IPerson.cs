using System;

namespace PizzaProject.Team1.People
{
    public interface IPerson 
    {
        Guid Id { get; }
        string Name { get; }
    }
}
