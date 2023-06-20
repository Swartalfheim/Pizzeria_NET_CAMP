using System.Collections.Concurrent;
using System.Text;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Enums;


namespace PizzaProject
{
    public class Storage
    {
        private ConcurrentDictionary<Ingredient, uint> _ingredientStorage = new();
        private ConcurrentDictionary<IOffer, uint> _preparedDishes = new();

        public ConcurrentDictionary<Order, uint> _preparedOrders = new();

        public ConcurrentDictionary<Order, uint> PreparedOrders => _preparedOrders;
        public ConcurrentDictionary<IOffer, uint> PreparedDishes => _preparedDishes;

        public Storage()
        {
            _ingredientStorage = new ConcurrentDictionary<Ingredient, uint>();
        }

        public void RequestIngredient()
        {
            List<string> ingredient = _ingredientStorage.Where(x => x.Value < 30).Select(x => x.Key.Name).ToList();

            Dictionary<Ingredient, uint> ingredientNew = Filler.ResponsByIngredient(ingredient);

            foreach (var item in ingredientNew)
            {
                PutIngredient(item.Key, item.Value);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Storage Reneve ingredients!!!");
            Console.ResetColor();
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

        public void PutDish(IOffer name)
        {
            _preparedDishes.AddOrUpdate(name, 1, (key, oldValue) => oldValue + 1);
        }
        public TakeResult TakeDish(IOffer dish, uint quantity = 1)
        {
            if (_preparedDishes.TryGetValue(dish, out uint oldValue))
            {
                if (oldValue >= quantity)
                {
                    uint newValue = oldValue - quantity;
                    if (_preparedDishes.TryUpdate(dish, newValue, oldValue))
                    {
                        return TakeResult.SuccessfullyTaken;
                    }
                }
                else
                {
                    return TakeResult.OutOfValue;
                }
            }

            return TakeResult.NotFound;
        }

        public TakeResult TakeIngredient(Ingredient ingredient, uint quantity = 1)
        {
            Ingredient keyIngredient = new Ingredient(ingredient.Name);
            if (!_ingredientStorage.ContainsKey(keyIngredient))
            {
                return TakeResult.NotFound;
            }

            if (_ingredientStorage[keyIngredient] == 0 || _ingredientStorage[keyIngredient] < quantity)
            {
                return TakeResult.OutOfValue;
            }

            _ingredientStorage[keyIngredient] -= quantity;
            return TakeResult.SuccessfullyTaken;
        }

        public void PutOrder(Order order)
        {
            _preparedOrders.AddOrUpdate(order, 1, (key, oldValue) => oldValue + 1);
        }

        public TakeResult TakeOrder(Order order, uint quantity = 1)
        {
            if (_preparedOrders.TryGetValue(order, out uint oldValue))
            {
                if (oldValue >= quantity)
                {
                    uint newValue = oldValue - quantity;
                    if (_preparedOrders.TryUpdate(order, newValue, oldValue))
                    {
                        return TakeResult.SuccessfullyTaken;
                    }
                }
                else
                {
                    return TakeResult.OutOfValue;
                }
            }

            return TakeResult.NotFound;
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
                foreach (KeyValuePair<IOffer, uint> item in _preparedDishes)
                {
                    result.Append($"{item.Key} - {item.Value}\n");
                }
            }
            return result.ToString();
        }

        // request 
    }
}