using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Team1.TemporaryClasses
{
    interface IMenu
    {
        IEnumerable<MenuItem> MenuItems { get; }
    }

    class Menu : IMenu
    {
        private HashSet<MenuItem> menuItems;
        public IEnumerable<MenuItem> MenuItems => menuItems;
    }
    class MenuItem
    {
        public IOffer Offer { get; }

        decimal Price { get; }
        public List<(Ingredient, decimal)> AdditionalIngredients { get; }

    }
}
