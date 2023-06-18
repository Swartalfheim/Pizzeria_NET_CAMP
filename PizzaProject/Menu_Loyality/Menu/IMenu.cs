using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Menu_Loyality.Loyality;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Menu_Loyality.Menu
{
    public interface IMenu
    {
        Dictionary<IOffer, decimal> Dishes { get; }

        Dictionary<IOffer, List<CostsOfIngredients>> AdditionalIngredients { get; }

        Dictionary<VipLvl, List<DiscountsOfOffer>> SpecialOffer { get; }
    }
}
