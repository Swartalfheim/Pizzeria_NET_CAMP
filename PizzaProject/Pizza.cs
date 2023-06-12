using PizzaProject.Temp;

namespace PizzaProject
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
        public string Name { get => _name; }
        public string Description { get => _description; }
        public Category Category { get => Category.Pizzas; }
        public Recipe Recipe { get => _recipe; }
        public PizzaDough Dough { get => _dough; }
        public IOffer.Size Size { get => _size; }
        public IEnumerable<IStorageable> AdditionalIngredients { get => _additionalIngredients; }
        public void AddAdditionalIngredients(List<IStorageable> ingredients)
        {
            _additionalIngredients.AddRange(ingredients);
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
