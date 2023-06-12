namespace PizzaProject
{
    public interface IHaveAdditionalIngredients
    {
        IEnumerable<IStorageable> AdditionalIngredients { get; }
        void AddAdditionalIngredients(List<IStorageable> ingredients);
    }
}
