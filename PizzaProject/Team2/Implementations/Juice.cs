using PizzaProject.Team2.Abstractions;

namespace PizzaProject.Team2.Implementations
{
    public class Juice : IOffer
    {
        public enum JuiceTaste
        {
            Apple,
            Orange,
            Pineapple
        }
        private string _name;
        private string _description;
        private Recipe _recipe;
        private JuiceTaste _taste;
        private IOffer.Size _size;
        public Juice(string name, string description, Recipe recipe, JuiceTaste taste, IOffer.Size size)
        {
            _name = name;
            _description = description;
            _recipe = recipe;
            _taste = taste;
            _size = size;
        }
        public string Name { get => _name; }
        public string Description { get => _description; }
        public PizzeriaData.Category Category { get => PizzeriaData.Category.Drinks; }
        public Recipe Recipe { get => _recipe; }
        public JuiceTaste Taste { get => _taste; }
        public IOffer.Size Size { get => _size; }
        public override string ToString()
        {
            return $"Juice \"{_name}\" - Taste: {_taste}, Size: {_size}";
        }
    }
}
