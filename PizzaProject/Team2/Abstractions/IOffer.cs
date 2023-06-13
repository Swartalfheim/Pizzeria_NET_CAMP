using PizzaProject.Team2.Implementations;

namespace PizzaProject.Team2.Abstractions
{
    public interface IOffer : IStorageable
    {
        enum Size
        {
            ExtraLarge,
            Large,
            Medium,
            Small
        }
        string Description { get; }
        PizzeriaData.Category Category { get; }
        Recipe Recipe { get; }
    }
}
