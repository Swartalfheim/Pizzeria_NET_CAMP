using PizzaProject.Dishes_Orders.Abstractions;

namespace PizzaProject.Menu_Loyality.Loyality
{
    internal class DiscountsOfOffer : IEquatable<DiscountsOfOffer>
    {
        private IOffer _offer;
        private ushort _discount;

        public ushort Discount { get => _discount; set => _discount = value; }
        internal IOffer Offer { get => _offer; set => _offer = value; }

        public DiscountsOfOffer(IOffer offer, ushort amount)
        {
            Offer = offer;
            Discount = amount;
        }

        public DiscountsOfOffer(DiscountsOfOffer amountsOfOffer)
        {
            _offer = amountsOfOffer.Offer;
            _discount = amountsOfOffer.Discount;
        }

        public bool Equals(DiscountsOfOffer? other)
        {
            return _offer.Equals(other.Offer) && _discount.Equals(other.Discount);
        }
    }
}
