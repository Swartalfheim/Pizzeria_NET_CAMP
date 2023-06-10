using PizzaProject.Team1.TemporaryClasses;
namespace PizzaProject.Team1.CashRegisters
{
    public delegate void OrderApperiance(object order);
    internal interface ICashRegister
    {
        IMenu Menu { get; }
        IEnumerable<Wallet.PaymentCategory> PaymentMethod { get; }
        int CustomersInQueue { get; }

        void AddToQueue(object customer);
        void DeleteFromQueue(object customer);

        void MakeOrder(Customer customer);  //хто створює чергу? якщо автоматично будуть підходити до каси, то можна і без цього методу, ми вже будемо зберігати цього клієнта
        bool AddDishToOrder(IOffer dish, List<Ingredient> additional); //можливо трохи інші параметри в залежності від організації меню
        bool Pay(Wallet customerWallet);

        event OrderApperiance NewOrderApperiance;
    }
}
