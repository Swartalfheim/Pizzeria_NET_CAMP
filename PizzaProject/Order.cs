namespace PizzaProject
{
    public class Order
    {
        private Guid _id;
        private List<KeyValuePair<IOffer, uint>> _foodSet;
        public Order(List<KeyValuePair<IOffer, uint>> foodSet)
        {
            _id = Guid.NewGuid();
            _foodSet = new List<KeyValuePair<IOffer, uint>>(foodSet);
        }
        public IEnumerable<KeyValuePair<IOffer, uint>> FoodSet { get => _foodSet; }
        public decimal TotalPrice { get => throw new NotImplementedException(); } // price?
        public void Add(IOffer dish, uint amount)
        {
            if (amount > 0)
            {
                _foodSet.Add(new KeyValuePair<IOffer, uint>(dish, amount));
            }
        }
    }
}
