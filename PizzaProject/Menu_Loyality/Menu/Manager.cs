using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Menu_Loyality.Loyality;
using PizzaProject.Storage_Waiter.Interfaces;
using static PizzaProject.Administration.PizzeriaData;

namespace PizzaProject.Menu_Loyality.Menu
{                   
    
                                                                                /// <summary>
                                                                                /// Remake
                                                                                /// </summary>
    public class Manager : IStaff
    {
        private Menu _menu;
        public string Info => throw new NotImplementedException();

        private Dictionary<VipLvl, HashSet<IOffer>> _offersOnLvl = new Dictionary<VipLvl, HashSet<IOffer>>();

        public Manager(Menu menu)
        {
            _menu = menu;
        }
        public Manager(Guid id, string name, Menu menu)
        {
            _menu = menu;
            ///Complete constructor
        }
        public void AddSpecialOffer(IOffer offer, List<DiscountsOfOffer>? discountsOffers = null)
        {
            _menu.AddSpecialOffer(new Dictionary<VipLvl, List<DiscountsOfOffer>> {{
                _offersOnLvl.Where(x => x.Value.Contains(offer)).FirstOrDefault().Key
                , discountsOffers } });
        }

        public void SetSpecialOffer(IOffer offer, List<DiscountsOfOffer>? discountsOfOffers)
        {
            _menu.SetSpecialOffer(new Dictionary<VipLvl, List<DiscountsOfOffer>> {{
                _offersOnLvl.Where(x => x.Value.Contains(offer)).FirstOrDefault().Key
                , discountsOfOffers } });
        }

        public void RemoveSpecialOffer(IOffer offer, List<DiscountsOfOffer> discountsOfOffers)
        {
            _menu.RemoveSpecialOffer(new Dictionary<VipLvl, List<DiscountsOfOffer>> {{
                _offersOnLvl.Where(x => x.Value.Contains(offer)).FirstOrDefault().Key
                , discountsOfOffers } });
        }

        public void AddDish(Dictionary<IOffer, decimal> dish)
        {
            _menu.AddDish(dish);
        }

        public void SetDish(Dictionary<IOffer, decimal> dish)
        {
            _menu.SetDish(dish);
        }

        public void RemoveDish(Dictionary<IOffer, decimal> dish)
        {
            _menu.RemoveDish(dish);
        }

        public void AddIngredient(IOffer offer, List<CostsOfIngredients> costsOfIngredients) // remove-set
        {
            _menu.AddAditionalIngridient(new Dictionary<IOffer, List<CostsOfIngredients>> { { offer, costsOfIngredients } });
        }

        public void RemoveIngredient(IOffer offer, List<CostsOfIngredients> costsOfIngredients)
        {
            _menu.RemoveAditionalIngridient(new Dictionary<IOffer, List<CostsOfIngredients>> { { offer, costsOfIngredients } });
        }

        public void SetIngredient(IOffer offer, List<CostsOfIngredients> costsOfIngredients)
        {
            _menu.SetAditionalIngridient(new Dictionary<IOffer, List<CostsOfIngredients>> { { offer, costsOfIngredients } });
        }


        public void AddOfferOnLvl(VipLvl vipLvl, IOffer offer)
        {
            if (!_offersOnLvl.ContainsKey(vipLvl))
            {
                _offersOnLvl.Add(vipLvl, new HashSet<IOffer>() { offer });
            }
            else
            {
                _offersOnLvl[vipLvl].Add(offer);
            }
        }
    }
}
