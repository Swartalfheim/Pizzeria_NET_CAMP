using PizzaProject.Dishes_Orders.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject
{
    public static class Filler
    {
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
            Random random = new Random();

            Dictionary<Ingredient, uint> dictionary = GetIngredients().ToDictionary(
                ingredient => ingredient,
                ingredient => (uint)random.Next(10, 201)
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


            Recipe recipe1 = new Recipe("Pepperoni", ingrads1);

            Recipe recipe2 = new Recipe("Margarita", ingrads2);

            Recipe recipe3 = new Recipe("Juice", ingrads3);

            Dictionary<string, Recipe> result = new Dictionary<string, Recipe>();
            result.Add(recipe1.Name, recipe1);
            result.Add(recipe2.Name, recipe2);
            result.Add(recipe3.Name, recipe3);

            return result;
        }
    }
}
