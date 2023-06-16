namespace PizzaProject.Dishes_Orders.Abstractions
{
    public interface IHaveAdditionalIngredients
    {
        IEnumerable<IStorageable> AdditionalIngredients { get; }
        void AddAdditionalIngredients(List<IStorageable> ingredients);
    }
}
