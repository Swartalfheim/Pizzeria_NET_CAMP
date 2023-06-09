using PizzaProject.Team1.TemporaryClasses;
namespace PizzaProject.Team1.CashRegisters
{
    public delegate void OrderApperiance(object order);
    internal interface ICashRegister
    {
        IMenu GetMenu { get; }
        HashSet<Wallet.MoneyCategory> PaymentMethod { get; }
        int CustomersInQueue { get; }

        /// <summary>
        /// Оскільки нам потрібно щоб після оплати замовлення передавалось на кухню, 
        /// я подумав що було б добре щоб покупець передавав в параметрі саме замовлення.
        /// Тоді я  у методі оплати знімаю з рахунку покупця стільки грошех, скільки буде вказано в замовленні,
        /// заодно зможу спокійно викликати івент
        /// </summary>
        bool Pay(Wallet customerWallet, object order);
        HashSet<object> CheckCustomerStatus(object customer);
        void AddToQueue(object customer);
        void DeleteFromQueue(object customer);

        event OrderApperiance NewOrderApperiance;
    }
}
