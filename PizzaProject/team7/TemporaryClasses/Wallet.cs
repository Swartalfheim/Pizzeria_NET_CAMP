namespace PizzeriaTask_my_part_.TemporaryClasses
{
    public class Wallet
    {
        public enum PaymentCategory
        {
            Cash,
            PaymentService,
            Card
        }
        public Guid Id { get; private set; }

        private decimal _moneyAmount;

        //public decimal MoneyAmount => _moneyAmount; //тільки для відображення - погано

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

            //оповіщення симулятора/візуалізатора про поточний стан рахунку - event?

            return true;
        }
        public void ReceivePayment(decimal count)
        {
            _moneyAmount += count;

            //оповіщення симулятора/візуалізатора про поточний стан рахунку - event?
        }
    }
}