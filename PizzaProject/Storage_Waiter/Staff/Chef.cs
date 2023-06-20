using Newtonsoft.Json;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Storage_Waiter.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Storage_Waiter.Staff
{
    public class Chef : IStaff
    {
        [field: NonSerialized]
        public delegate void DishPreparedHandler(string chefName, string dishName);
        [field: NonSerialized]
        public event DishPreparedHandler? DishPrepared;
        [field: NonSerialized]
        public static event Action UpdateIngredient;

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Id")]
        public uint Id { get; private set; }

        [JsonIgnore]
        public bool IsBusy { get; set; }
        [JsonIgnore]
        //public List<Recipe> Recipes { get; set; } = new();
        public List<Category> Categories { get; set; }
        [JsonIgnore]
        private Storage _storage;

        //public Chef(string name, Storage storage, List<Recipe> recipes = null)
        //{
        //    Id = UniqueIntGenerator.GetUniqueChefInt();
        //    Name = name;
        //    Recipes = recipes ?? new List<Recipe>();
        //    _storage = storage;
        //}

        public Chef(string name, Storage storage, params Category[] categories)
        {
            Id = UniqueIntGenerator.GetUniqueChefInt();
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
            ConnectorHandler.UpdateChefData(this, dish.Name);
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
