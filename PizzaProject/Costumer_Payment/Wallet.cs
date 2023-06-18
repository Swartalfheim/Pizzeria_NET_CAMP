using System;

namespace PizzaProject.Costumer_Payment
{
    public class Wallet
    {
        public enum PaymentCategory
        {
            Cash, PaymentService, Card
        }
        public Guid Id { get; private set; }

        private decimal _moneyAmount;

        public PaymentCategory Category { get; private set; }

        public Wallet(decimal amount, PaymentCategory category)
        {
            _moneyAmount = amount;
            Category = category;
        }

        public bool SendPayment(decimal count, Wallet receiver)
        {
            if (_moneyAmount < count)
                return false;

            _moneyAmount -= count;
            receiver.ReceivePayment(count);
            return true;
        }
        public void ReceivePayment(decimal count)
        {
            _moneyAmount += count;
        }
    }
}