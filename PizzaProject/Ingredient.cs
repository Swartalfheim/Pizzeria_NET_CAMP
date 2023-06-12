namespace PizzaProject
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
