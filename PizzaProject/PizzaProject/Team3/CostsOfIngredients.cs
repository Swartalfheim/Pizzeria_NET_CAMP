using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria_NET_CAMP
{
    internal class CostsOfIngredients : IEquatable<CostsOfIngredients>
    {
        private Ingredients _ingredients;
        private decimal _cost;

        public decimal Cost { get => _cost; set => _cost = value; }
        internal Ingredients Ingredients { get => _ingredients; set => _ingredients = value; }

        public CostsOfIngredients(CostsOfIngredients costsOfIngredients) {
            _ingredients = costsOfIngredients.Ingredients;
            _cost = costsOfIngredients.Cost;
        }

        public CostsOfIngredients(Ingredients ingredients, decimal cost)
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
