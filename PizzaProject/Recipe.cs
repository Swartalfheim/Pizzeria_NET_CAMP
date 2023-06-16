
namespace PizzaProject
{
    public class Recipe
    {
        public string Name { get; set; }
        public Dictionary<Ingredient, uint> Ingredients { get; set; }

        public Recipe(string name, Dictionary<Ingredient, uint> ingredients)
        {
            Name = name;
            Ingredients = new Dictionary<Ingredient, uint> (ingredients);
        }
    }
}
