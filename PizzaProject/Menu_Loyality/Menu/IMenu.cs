using PizzaProject.Menu_Loyality.Loyality;

namespace PizzaProject.Menu_Loyality.Menu
{
    internal interface IMenu
    {
        Dictionary<IOffer, decimal> Dishes { get; }

        Dictionary<IOffer, List<CostsOfIngredients>> AdditionalIngredients { get; }

        Dictionary<PizzeriaData.VipLvl, List<DiscountsOfOffer>> SpecialOffer { get; }
    }
}
