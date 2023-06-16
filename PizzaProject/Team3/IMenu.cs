using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria_NET_CAMP
{
    internal interface IMenu
    {
        Dictionary<IOffer, decimal> Dishes { get; }

        Dictionary<IOffer, List<CostsOfIngredients>> AdditionalIngredients { get; }

        Dictionary<PizzeriaData.VipLvl, List<DiscountsOfOffer>> SpecialOffer { get; }
    }
}
