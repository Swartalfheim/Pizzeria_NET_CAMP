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
        PizzeriaData.Category Category { get; }
        Recipe Recipe { get; }
    }
}
