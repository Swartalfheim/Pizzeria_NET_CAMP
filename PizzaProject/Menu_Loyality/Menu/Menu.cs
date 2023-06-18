using PizzaProject.Administration;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Menu_Loyality.Loyality;
using static PizzaProject.Administration.PizzeriaData;


namespace PizzaProject.Menu_Loyality.Menu
{
    public class Menu : IMenu
    {
        private static Menu _instance;

        private Dictionary<IOffer, decimal> _dishes = new Dictionary<IOffer, decimal>();

        private Dictionary<IOffer, List<CostsOfIngredients>> _additionalIngredients = new Dictionary<IOffer, List<CostsOfIngredients>>();

        private Dictionary<VipLvl, List<DiscountsOfOffer>> _specialOffer = new Dictionary<VipLvl, List<DiscountsOfOffer>>();

        public Dictionary<IOffer, decimal> Dishes => new Dictionary<IOffer, decimal>(_dishes);

        public Dictionary<IOffer, List<CostsOfIngredients>> AdditionalIngredients => new Dictionary<IOffer, List<CostsOfIngredients>>(_additionalIngredients);

        public Dictionary<VipLvl, List<DiscountsOfOffer>> SpecialOffer => new Dictionary<VipLvl, List<DiscountsOfOffer>>(_specialOffer);

        private Menu() 
        {

            Dictionary<string, Recipe> resultRecepie = Filler.GetForFirstChef();


            Pizza pizza1 = new Pizza("Margarita", "First pizza", resultRecepie["Margarita"], Pizza.PizzaDough.Thick, IOffer.Size.Large);
            Pizza pizza2 = new Pizza("Pepperoni", "Second pizza", resultRecepie["Pepperoni"], Pizza.PizzaDough.WithFilling, IOffer.Size.Small);

            Juice juice1 = new Juice("Juice", "Delicius juice", resultRecepie["Juice"], Juice.JuiceTaste.Orange, IOffer.Size.Large);


            _dishes.Add(pizza1, 550);
            _dishes.Add(pizza2, 450);
            _dishes.Add(juice1, 300);

        }

        public List<CostsOfIngredients> GetAdditional(IOffer offer)
        {
            return _additionalIngredients[offer];
        }

        //public static (Dictionary<IOffer, decimal> _dishes, Dictionary<IOffer, uint>) GetInstance()
        //{
        //    Dictionary<IOffer, uint> temp = new Dictionary<IOffer, uint>();
        //    foreach (var ingredient in _additionalIngredients)
        //    {
        //        temp.Add(ingredient.Key, (uint)ingredient.Value.Count);
        //    }
        //    return (_dishes, temp);
        //}

        public static Menu GetInstance()
        {
            if (_instance == null)
                _instance = new Menu();
            return _instance;
        }

        public void AddSpecialOffer(Dictionary<VipLvl, List<DiscountsOfOffer>> offer)
        {
            if (offer is null)
            {
                throw new ArgumentNullException(nameof(offer));
            }
            foreach (var element in offer)
            {
                if (!_specialOffer.ContainsKey(element.Key))
                {
                    _specialOffer.Add(element.Key, element.Value);
                }
                else
                {
                    foreach (var items in element.Value)
                    {
                        _specialOffer[element.Key].Add(new DiscountsOfOffer(items));
                    }
                }

            }
        }
        public void RemoveSpecialOffer(Dictionary<VipLvl, List<DiscountsOfOffer>> offer)
        {
            if (offer is null)
            {
                throw new ArgumentNullException(nameof(offer));
            }
            foreach (var element in offer)
            {
                if (!_specialOffer.ContainsKey(element.Key))
                {
                    throw new ArgumentException($"No offers for {element.Key}!");
                }
                foreach (var offers in element.Value)
                {
                    _specialOffer[element.Key].Remove(offers);
                }
            }
        }

        public void SetSpecialOffer(Dictionary<VipLvl, List<DiscountsOfOffer>> offer)
        {
            if (offer is null)
            {
                throw new ArgumentNullException(nameof(offer));
            }
            foreach (var element in offer)
            {

                if (!_specialOffer.ContainsKey(element.Key))
                {
                    _specialOffer.Add(element.Key, element.Value);
                }
                else
                {
                    foreach (var items in element.Value)
                    {
                        if (!element.Value.Contains(items))
                        {
                            _specialOffer[element.Key] = element.Value;
                        }
                        else
                        {
                            throw new Exception("Non existing offer");
                        }
                    }
                }
            }
        }

        public void AddDish(Dictionary<IOffer, decimal> dish)
        {
            if (dish is null)
            {
                throw new ArgumentNullException(nameof(dish));
            }
            foreach (var item in dish)
            {
                _dishes.Add(item.Key, item.Value);
            }
        }

        public void RemoveDish(Dictionary<IOffer, decimal> dish)
        {
            if (dish is null)
            {
                throw new ArgumentNullException(nameof(dish));
            }
            foreach (var item in dish)
            {
                _dishes.Remove(item.Key);
            }
        }

        public void SetDish(Dictionary<IOffer, decimal> dish)
        {
            if (dish is null)
            {
                throw new ArgumentNullException(nameof(dish));
            }
            foreach (var item in dish)
            {
                _dishes[item.Key] = item.Value;
            }
        }

        public void AddAditionalIngridient(Dictionary<IOffer, List<CostsOfIngredients>> additional)
        {
            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            foreach (var item in additional)
            {
                if (!_additionalIngredients.ContainsKey(item.Key))
                {
                    _additionalIngredients.Add(item.Key, item.Value);
                }
                else
                {
                    foreach (var ingredient in item.Value)
                    {
                        _additionalIngredients[item.Key].Add(ingredient);
                    }
                }
            }
        }
        public void RemoveAditionalIngridient(Dictionary<IOffer, List<CostsOfIngredients>> additional)
        {
            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            foreach (var offers in additional)
            {
                foreach (var element in offers.Value)
                {
                    _additionalIngredients[offers.Key].Remove(element);
                }
            }
        }
        public void SetAditionalIngridient(Dictionary<IOffer, List<CostsOfIngredients>> additional)
        {
            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            foreach (var offers in additional)
            {
                _additionalIngredients[offers.Key] = offers.Value;
            }
        }
    }
}
