using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Implementations;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Dishes_Orders.Abstractions
{
    public interface IOffer : IStorageable, IEquatable<IOffer>
    {
        enum Size
        {
            ExtraLarge,
            Large,
            Medium,
            Small
        }
        string Description { get; }
        Category Category { get; }
        Recipe Recipe { get; }
    }
}
