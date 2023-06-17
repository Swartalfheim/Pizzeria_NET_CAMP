using System.Collections.Concurrent;
using System.Text;
using PizzaProject.Enums;

namespace PizzaProject
{
    public class Storage
    {
        private ConcurrentDictionary<Ingredient, uint> _ingredientStorage = new();
        private ConcurrentDictionary<string, uint> _preparedDishes = new();

        public Storage()
        {
            _ingredientStorage = new ConcurrentDictionary<Ingredient, uint>();
        }

        public Storage(Dictionary<Ingredient, uint> ingredientStorage)
        {
            if (ingredientStorage == null)
            {
                throw new ArgumentNullException(nameof(ingredientStorage));
            }

            _ingredientStorage = new ConcurrentDictionary<Ingredient, uint>(ingredientStorage);
        }

        public void PutIngredient(Ingredient ingredient, uint quantity = 1)
        {
            _ingredientStorage.AddOrUpdate(ingredient, quantity, (key, oldValue) => oldValue + quantity);
        }

        public void PutDish(string name)
        {
            _preparedDishes.AddOrUpdate(name, 1, (key, oldValue) => oldValue + 1);
        }
        public TakeResult TakeDish(string dishName, uint quantity = 1)
        {
            if (_preparedDishes.TryGetValue(dishName, out uint oldValue))
            {
                if (oldValue >= quantity)
                {
                    uint newValue = oldValue - quantity;
                    if (_preparedDishes.TryUpdate(dishName, newValue, oldValue))
                    {
                        return TakeResult.SuccessfullyTaken;
                    }
                }
                else
                {
                    return TakeResult.OutOfValue;
                }
            }

            return TakeResult.NotFoundIngredient;
        }

        public TakeResult TakeIngredient(Ingredient ingredient, uint quantity = 1)
        {
            Ingredient keyIngredient = new Ingredient { Name = ingredient.Name };
            if (!_ingredientStorage.ContainsKey(keyIngredient))
            {
                return TakeResult.NotFoundIngredient;
            }

            if (_ingredientStorage[keyIngredient] == 0 || _ingredientStorage[keyIngredient] < quantity)
            {
                return TakeResult.OutOfValue;
            }

            _ingredientStorage[keyIngredient] -= quantity;
            return TakeResult.SuccessfullyTaken;
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