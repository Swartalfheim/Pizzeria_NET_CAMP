using Newtonsoft.Json;
using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Abstractions;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Dishes_Orders.Implementations
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
        [JsonProperty("Name")]
        public string Name { get => _name; }
        [JsonProperty("Description")]
        public string Description { get => _description; }
        [JsonProperty("Category")]
        public Category Category { get => Category.Drinks; }
        public Recipe Recipe { get => _recipe; }
        [JsonProperty("Dough")]
        public JuiceTaste Taste { get => _taste; }
        [JsonProperty("Size")]
        public IOffer.Size Size { get => _size; }
        public override string ToString()
        {
            return $"Juice \"{_name}\" - Taste: {_taste}, Size: {_size}";
        }
        public bool Equals(IOffer? other)
        {
            return _name == other?.Name;
        }
    }
}
