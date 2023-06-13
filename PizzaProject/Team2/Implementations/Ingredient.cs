using PizzaProject.Team2.Abstractions;

namespace PizzaProject.Team2.Implementations
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
