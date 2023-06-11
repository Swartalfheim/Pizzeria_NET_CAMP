namespace PizzaProject
{
    public class Storage
    {
        private Dictionary<Ingredient, uint> _ingredientStorage;

        public Storage()
        {
            _ingredientStorage = new Dictionary<Ingredient, uint>
            {
                { new Ingredient { Name = "Ham" }, 2},
                { new Ingredient { Name = "Cheese" }, 4},
                { new Ingredient { Name = "Sausage" }, 1},
                { new Ingredient { Name = "Pepper" }, 0},
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

        public void PutIngredient(Ingredient ingredient)
        {
            if (!_ingredientStorage.ContainsKey(ingredient))
            {
                _ingredientStorage.Add(ingredient, 1);
                return;
            }

            _ingredientStorage[ingredient] += 1;
        }

        public TakeIngredientResult TakeIngredient(string ingredientName)
        {
            Ingredient keyIngredient = new Ingredient { Name = ingredientName };
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
    }
}