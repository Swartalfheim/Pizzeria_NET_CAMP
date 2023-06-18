using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Storage_Waiter.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Storage_Waiter.Staff
{
    public class Chef : IStaff
    {
        public delegate void DishPreparedHandler(string chefName, string dishName);
        public event DishPreparedHandler? DishPrepared;
        public static event Action UpdateIngredient;

        public string Name { get; set; }
        public bool IsBusy { get; set; }
        public Dictionary<string, Recipe> Recipes { get; set; } = new Dictionary<string, Recipe>();

        private Storage _storage;

        public Chef(string name, Storage storage, Dictionary<string, Recipe> recipes = null)
        {
            Name = name;
            Recipes = recipes ?? new Dictionary<string, Recipe>();
            _storage = storage;

        }

        public void Cook(string dishName)
        {
            if (!Recipes.ContainsKey(dishName))
            {
                throw new Exception("Recipe not found");
            }

            Recipe recipe = Recipes[dishName];

            if (!_storage.CheckIngredientsAvailability(recipe)) // перевірка чи є інгредієнти на складі
            {
                UpdateIngredient?.Invoke();

                Thread.Sleep(50);

                throw new Exception($"Not all ingredients are available for the dish {dishName}");
                
            }

            foreach (KeyValuePair<Ingredient, uint> ingredient in recipe.Ingredients)
            {
                for (int i = 0; i < ingredient.Value; i++)
                {
                    _storage.TakeIngredient(ingredient.Key);
                }
            }

            Thread.Sleep(3000); // симуляція часу приготування
            _storage.PutDish(dishName);
            DishPrepared?.Invoke(Name, dishName);

            IsBusy = false;  // зайнятість кухаря негативна
        }

        public string Info
        {
            get
            {
                string dishes = string.Join(", ", Recipes.Keys);
                return $"{Name}'s dishes: {dishes}";
            }
        }

        
    }
}
