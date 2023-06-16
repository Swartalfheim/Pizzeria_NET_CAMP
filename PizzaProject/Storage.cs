using System.Text;
using PizzaProject.Enums;

namespace PizzaProject
{
    public class Storage
    {
        private Dictionary<Ingredient, uint> _ingredientStorage;
        private Dictionary<string, uint> _preparedDishes = new Dictionary<string, uint>();

        public Storage()
        {
            _ingredientStorage = new Dictionary<Ingredient, uint>
            {
                { new Ingredient { Name = "Cheese" }, 2},
                { new Ingredient { Name = "Tomato" }, 6},
                { new Ingredient { Name = "Dough" }, 2},
                { new Ingredient { Name = "Milk" }, 1},
            };
        }

        public Storage(Dictionary<Ingredient, uint> ingredientStorage)
        {
            if (ingredientStorage == null)
            {
                throw new ArgumentNullException(nameof(ingredientStorage));
            }
            
            _ingredientStorage = new Dictionary<Ingredient, uint>(ingredientStorage);
        }

        public void PutIngredient(Ingredient ingredient, uint quantity = 1)
        {
            if (!_ingredientStorage.ContainsKey(ingredient))
            {
                _ingredientStorage.Add(ingredient, quantity);
                return;
            }

            _ingredientStorage[ingredient] += quantity;
        }

        public void PutDish(string name)
        {
            if (_preparedDishes.ContainsKey(name))
            {
                _preparedDishes[name]++;
            }
            else
            {
                _preparedDishes.Add(name, 1);
            }
        }

        public TakeIngredientResult TakeIngredient(Ingredient ingredient)
        {
            Ingredient keyIngredient = new Ingredient { Name = ingredient.Name };
            if (!_ingredientStorage.ContainsKey(keyIngredient))
            {
                return TakeIngredientResult.NotFoundIngredient;
            }

            if (_ingredientStorage[keyIngredient] == 0)
            {
                return TakeIngredientResult.OutOfIngredient;
            }

            _ingredientStorage[keyIngredient] -= 1;
            return TakeIngredientResult.SuccessfullyTaken;
        }

        public bool CheckIngredientsAvailability(Recipe recipe)
        {
            foreach (KeyValuePair<Ingredient, uint> ingredient in recipe.Ingredients)
            {
                if (!_ingredientStorage.ContainsKey(ingredient.Key) || _ingredientStorage[ingredient.Key] < ingredient.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("Ingredients and parts of dishes:\n");

            foreach (KeyValuePair<Ingredient, uint> item in _ingredientStorage)
            {
                result.Append($"{item.Key} - {item.Value}\n");
            }
            result.Append("\nReady dishes:\n");

            if (_preparedDishes.Count is 0)
            {
                result.Append("Empty");
            }
            else
            {
                foreach (KeyValuePair<string, uint> item in _preparedDishes)
                {
                    result.Append($"{item.Key} - {item.Value}\n");
                }
            }
            return result.ToString();
        }
    }
}