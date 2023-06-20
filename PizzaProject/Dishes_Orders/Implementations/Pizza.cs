using Newtonsoft.Json;
using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Abstractions;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Dishes_Orders.Implementations
{
    public class Pizza : IOffer, IHaveAdditionalIngredients
    {
        public enum PizzaDough
        {
            Thin,
            Thick,
            WithFilling
        }
        private string _name;
        private string _description;
        private Recipe _recipe;
        private PizzaDough _dough;
        private IOffer.Size _size;
        private List<IStorageable> _additionalIngredients;


        public Pizza(string name, string description, Recipe recipe, PizzaDough dough, IOffer.Size size, List<IStorageable>? additionalIngredients = null)
        {
            _name = name;
            _description = description;
            _recipe = recipe;
            _dough = dough;
            _size = size;
            _additionalIngredients = new List<IStorageable>();
            if (additionalIngredients != null)
            {
                _additionalIngredients.AddRange(additionalIngredients);
            }
        }
        [JsonProperty("Name")]
        public string Name { get => _name; }
        [JsonProperty("Description")]
        public string Description { get => _description; }
        [JsonProperty("Category")]
        public Category Category { get => Category.Pizzas; }
        [JsonIgnore]
        public Recipe Recipe { get => _recipe; }
        [JsonProperty("Dough")]
        public PizzaDough Dough { get => _dough; }
        [JsonProperty("Size")]
        public IOffer.Size Size { get => _size; }
        [JsonIgnore]
        public IEnumerable<IStorageable> AdditionalIngredients { get => _additionalIngredients; }

        

        public void AddAdditionalIngredients(List<IStorageable> ingredients)
        {
            _additionalIngredients.AddRange(ingredients);
        }

        public override string ToString()
        {
            string text = $"Pizza \"{_name}\" - Dough: {_dough}, Size: {_size}";
            if (_additionalIngredients.Any())
            {
                text += ", Additional ingredients: ";
                for (int i = 0; i < _additionalIngredients.Count - 1; i++)
                {
                    text += $"{_additionalIngredients[i].Name}, ";
                }
                text += $"{_additionalIngredients[^1].Name}";
            }
            return text;
        }

        public bool Equals(IOffer? other)
        {
            return _name == other?.Name;
        }
    }
}
