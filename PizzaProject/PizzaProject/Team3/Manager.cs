using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria_NET_CAMP
{
    internal class Manager : IStaff
    {
        private Menu _menu;
        public string Info => throw new NotImplementedException();

        private Dictionary<PizzeriaData.VipLvl, HashSet<IOffer>> _offersOnLvl = new Dictionary<PizzeriaData.VipLvl, HashSet<IOffer>>();

        public Manager(Menu menu) {
            _menu = menu; 
        }

        public void AddSpecialOffer(IOffer offer, List<DiscountsOfOffer>? discountsOffers = null)
        {
            _menu.AddSpecialOffer( new Dictionary<PizzeriaData.VipLvl, List<DiscountsOfOffer>> {{
                _offersOnLvl.Where(x => x.Value.Contains(offer)).FirstOrDefault().Key
                , discountsOffers } } );
        }

        public void SetSpecialOffer(IOffer offer, List<DiscountsOfOffer>? discountsOfOffers)
        {
            _menu.SetSpecialOffer(new Dictionary<PizzeriaData.VipLvl, List<DiscountsOfOffer>> {{
                _offersOnLvl.Where(x => x.Value.Contains(offer)).FirstOrDefault().Key
                , discountsOfOffers } });
        }

        public void RemoveSpecialOffer(IOffer offer, List<DiscountsOfOffer> discountsOfOffers) {
            _menu.RemoveSpecialOffer(new Dictionary<PizzeriaData.VipLvl, List<DiscountsOfOffer>> {{
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
            _menu.AddAditionalIngridient(new Dictionary<IOffer, List<CostsOfIngredients>> { { offer,  costsOfIngredients } });
        }

        public void RemoveIngredient(IOffer offer, List<CostsOfIngredients> costsOfIngredients)
        {
            _menu.RemoveAditionalIngridient(new Dictionary<IOffer, List<CostsOfIngredients>> { { offer, costsOfIngredients } });
        }

        public void SetIngredient(IOffer offer, List<CostsOfIngredients> costsOfIngredients)
        {
            _menu.SetAditionalIngridient(new Dictionary<IOffer, List<CostsOfIngredients>> { { offer, costsOfIngredients } });
        }


        public void AddOfferOnLvl(PizzeriaData.VipLvl vipLvl, IOffer offer) 
        {
            if (!_offersOnLvl.ContainsKey(vipLvl))
            {
                _offersOnLvl.Add(vipLvl, new HashSet<IOffer>() { offer});
            }
            else
            {
                _offersOnLvl[vipLvl].Add(offer);
            }
        }
    }
}
