using PizzaProject.Dishes_Orders.Implementations;

namespace PizzaProject.Menu_Loyality.Menu
{
    public class CostsOfIngredients : IEquatable<CostsOfIngredients>
    {
        private Ingredient _ingredients;
        private decimal _cost;

        public decimal Cost { get => _cost; set => _cost = value; }
        internal Ingredient Ingredients { get => _ingredients; set => _ingredients = value; }

        public CostsOfIngredients(CostsOfIngredients costsOfIngredients)
        {
            _ingredients = costsOfIngredients.Ingredients;
            _cost = costsOfIngredients.Cost;
        }

        public CostsOfIngredients(Ingredient ingredients, decimal cost)
        {
            Ingredients = ingredients;
            Cost = cost;
        }

        public bool Equals(CostsOfIngredients? other)
        {
            return _ingredients.Equals(other.Ingredients) && _cost == other.Cost;
        }
    }
}
