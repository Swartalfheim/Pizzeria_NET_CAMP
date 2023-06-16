using PizzaProject.Dishes_Orders.Abstractions;

namespace PizzaProject.Dishes_Orders.Implementations
{
    public class Ingredient : IStorageable
    {
        private string _name;
        public Ingredient(string name)
        {
            _name = name;
        }
        public string Name { get => _name; }
    }
}
