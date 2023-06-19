using PizzaProject.Dishes_Orders.Abstractions;
using System.Text;
using System.Xml.Linq;

namespace PizzaProject.Dishes_Orders.Implementations
{
    public class Order
    {
        private static uint orders = 0;
        private uint _id;
        private List<KeyValuePair<IOffer, uint>> _foodSet = new List<KeyValuePair<IOffer, uint>>();
        public decimal TotalPrice { get; set; }

        public Order()
        {
            _id = UniqueIntGenerator.GetUniqueInt();
            //_id = ++orders; //потоки
        }

        public uint Id
        {
            get { return _id; }
        }

        public Order(List<KeyValuePair<IOffer, uint>> foodSet)
        {
            _id = UniqueIntGenerator.GetUniqueInt();
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

        public override string? ToString()
        {
            StringBuilder sb = new StringBuilder($"[ID:{_id}] ");
            foreach (var item in _foodSet)
            {
                sb.Append($"{item.Key.Name} ");
            }
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Order other = (Order)obj;
            return _id == other.Id;
        }

        public override int GetHashCode()
        {
            return (int)_id;
        }


        ////ціни треба брати з меню (замість Dictionary<IOffer, decimal> priceList)
        //public decimal GetTotalPrice()
        //{
        //    try
        //    {
        //        return _foodSet.Select(f => priceList[f.Key] * f.Value).Sum();
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
    }
}
