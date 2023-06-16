using PizzaProject.Costumer_Payment.People;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Menu_Loyality.Menu;

namespace PizzaProject.Costumer_Payment.CashRegisters
{
    public delegate void OrderApperiance(Order order);
    internal interface ICashRegister
    {
        IMenu GetMenu { get; }
        IEnumerable<Wallet.PaymentCategory> PaymentMethod { get; }
        int CustomersInQueue { get; }

        void AddToQueue(Customer customer);
        void DeleteFromQueue(Customer customer);

        void MakeOrder(Customer customer);  //хто створює чергу? якщо автоматично будуть підходити до каси, то можна і без цього методу, ми вже будемо зберігати цього клієнта
        bool AddDishToOrder(IOffer dish, List<Ingredient> additional); //можливо трохи інші параметри в залежності від організації меню
        bool Pay(Wallet customerWallet);

        event OrderApperiance NewOrderApperiance;
    }
}
