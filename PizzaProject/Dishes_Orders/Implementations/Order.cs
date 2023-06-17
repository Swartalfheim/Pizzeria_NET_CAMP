using PizzaProject.Dishes_Orders.Abstractions;

namespace PizzaProject.Dishes_Orders.Implementations
{
    public class Order
    {
        private static uint orders = 0;
        private uint _id;
        private List<KeyValuePair<IOffer, uint>> _foodSet;
        public decimal TotalPrice => GetTotalPrice();

        public Order()
        {
            _id = ++orders; //потоки
        }

        public Order(List<KeyValuePair<IOffer, uint>> foodSet)
        {
            _id = Guid.NewGuid();
            _foodSet = new List<KeyValuePair<IOffer, uint>>(foodSet);
        }
        public IEnumerable<KeyValuePair<IOffer, uint>> FoodSet { get => _foodSet; }


        public void Add(IOffer dish, uint amount)
        {
            if (amount > 0)
            {
                _foodSet.Add(new KeyValuePair<IOffer, uint>(dish, amount));
            }
        }

        //ціни треба брати з меню (замість Dictionary<IOffer, decimal> priceList)
        public decimal GetTotalPrice()
        {
            //try
            //{
            //    return _foodSet.Select(f => priceList[f.Key] * f.Value).Sum();
            //}
            //catch
            //{
            //    return 0;
            //}
        }
    }
}
