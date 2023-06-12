using PizzaProject.Temp;

namespace PizzaProject
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
        Category Category { get; }
        Recipe Recipe { get; }
    }
}
