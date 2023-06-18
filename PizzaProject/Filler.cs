using PizzaProject.Dishes_Orders.Implementations;

namespace PizzaProject
{
    public static class Filler
    {
        private static Random _random = new Random();
        public static List<Ingredient> GetIngredients()
        {
            List<Ingredient> _ingredients = new List<Ingredient>
            {
                { new Ingredient("Tomato") },
                { new Ingredient("Beef") },
                { new Ingredient("Mushroom") },
                { new Ingredient("Orange") },
                { new Ingredient("Milk") },
                { new Ingredient("Cheese") },
            };

            return _ingredients;
        }

        public static Dictionary<Ingredient, uint> GetIngredientForStorage()
        {

            Dictionary<Ingredient, uint> dictionary = GetIngredients().ToDictionary(
                ingredient => ingredient,
                ingredient => (uint)_random.Next(40, 100)
            );

            return dictionary;
        }

        public static Dictionary<Ingredient, uint> ResponsByIngredient(List<string> ingrIncome)
        {
            List<Ingredient> _ingredients = GetIngredients();

            Dictionary<Ingredient, uint> dictionary = GetIngredients().Where(x => ingrIncome.Contains(x.Name)).ToDictionary(
                ingredient => ingredient,
                ingredient => (uint)_random.Next(5, 20)
            );

            return dictionary;
        }


        public static Dictionary<string, Recipe> GetForFirstChef()
        {
            Ingredient ingredient1 = new Ingredient("Tomato");
            Ingredient ingredient2 = new Ingredient("Beef");
            Ingredient ingredient3 = new Ingredient("Mushroom");
            Ingredient ingredient4 = new Ingredient("Orange");

            Dictionary<Ingredient, uint> ingrads1 = new Dictionary<Ingredient, uint>();
            ingrads1.Add(ingredient1, 3);
            ingrads1.Add(ingredient2, 1);
            ingrads1.Add(ingredient3, 2);

            Dictionary<Ingredient, uint> ingrads2 = new Dictionary<Ingredient, uint>();
            ingrads2.Add(ingredient1, 3);
            ingrads2.Add(ingredient2, 1);

            Dictionary<Ingredient, uint> ingrads3 = new Dictionary<Ingredient, uint>();
            ingrads3.Add(ingredient4, 4);


            Recipe recipe1 = new Recipe("Pepperoni", ingrads1, 4);

            Recipe recipe2 = new Recipe("Margarita", ingrads2, 3);

            Recipe recipe3 = new Recipe("Juice", ingrads3, 1);

            Dictionary<string, Recipe> result = new Dictionary<string, Recipe>();
            result.Add(recipe1.Name, recipe1);
            result.Add(recipe2.Name, recipe2);
            result.Add(recipe3.Name, recipe3);

            return result;
        }
    }
}
