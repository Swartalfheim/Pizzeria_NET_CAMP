using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Storage_Waiter.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Storage_Waiter.Staff
{
    public class Chef : IStaff
    {
        public delegate void DishPreparedHandler(string chefName, string dishName);
        public event DishPreparedHandler? DishPrepared;
        public static event Action? UpdateIngredient;

        public string Name { get; set; }
        public bool IsBusy { get; set; }

        private Storage _storage;
        public List<Category> Categories { get; set; }

        public Chef(string name, Storage storage, params Category[] categories)
        {
            Name = name;
            _storage = storage;
            Categories = categories.ToList();
        }

        public void Cook(IOffer dish)
        {
            if (!Categories.Contains(dish.Category))
            {
                throw new Exception("Recipe not found");
            }

            if (!_storage.CheckIngredientsAvailability(dish.Recipe)) // перевірка чи є інгредієнти на складі
            {
                UpdateIngredient?.Invoke();
                throw new Exception($"Not all ingredients are available for the dish {dish.Name}");            
            }

            foreach (KeyValuePair<Ingredient, uint> ingredient in dish.Recipe.Ingredients)
            {
                _storage.TakeIngredient(ingredient.Key, ingredient.Value);
            }

            Thread.Sleep(((int)dish.Recipe.Time)); // симуляція часу приготування
            _storage.PutDish(dish);
            DishPrepared?.Invoke(Name, dish.Name);

            IsBusy = false;  // зайнятість кухаря негативна
        }

        public string Info
        {
            get
            {
                string dishes = string.Join(", ", Categories);
                return $"{Name}'s dishes: {dishes}";
            }
        }       
    }
}
