using PizzaProject.Dishes_Orders.Abstractions;

namespace PizzaProject.Dishes_Orders.Implementations
{
    public class Ingredient : IStorageable
    {
        private string _name;

        public string Name { get => _name; }

        public Ingredient(string name)
        {
            _name = name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Ingredient)
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    } 
}
