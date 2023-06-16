using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Implementations;

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
        PizzeriaData.Category Category { get; }
        Recipe Recipe { get; }
    }
}
