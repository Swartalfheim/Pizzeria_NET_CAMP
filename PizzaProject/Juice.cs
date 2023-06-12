using PizzaProject.Temp;

namespace PizzaProject
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
        public Category Category { get => Category.Drinks; }
        public Recipe Recipe { get => _recipe; }
        public JuiceTaste Taste { get => _taste; }
        public IOffer.Size Size { get => _size; }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
