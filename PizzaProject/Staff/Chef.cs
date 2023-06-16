namespace PizzaProject.Staff
{
    public class Chef
    {
        public delegate void DishPreparedHandler(string chefName, string dishName);
        public event DishPreparedHandler? DishPrepared;

        public string Name { get; set; }
        public bool IsBusy { get; set; }
        public Dictionary<string, Recipe> Recipes { get; set; } = new Dictionary<string, Recipe>();

        public Chef(string name, Dictionary<string, Recipe> recipes = null)
        {
            Name = name;
            Recipes = recipes ?? new Dictionary<string, Recipe>();
        }

        public void Cook(string dishName)
        {
            if (!Recipes.ContainsKey(dishName))
            {
                throw new Exception("Recipe not found");
            }

            Recipe recipe = Recipes[dishName];

            if (!PizzeriaData.Storage.CheckIngredientsAvailability(recipe)) // перевірка чи є інгредієнти на складі
            {
                throw new Exception($"Not all ingredients are available for the dish {dishName}");
            }

            foreach (KeyValuePair<Ingredient, uint> ingredient in recipe.Ingredients)
            {
                for (int i = 0; i < ingredient.Value; i++)
                {
                    PizzeriaData.Storage.TakeIngredient(ingredient.Key);
                }
            }

            Thread.Sleep(3000); // симуляція часу приготування
            PizzeriaData.Storage.PutDish(dishName);
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
