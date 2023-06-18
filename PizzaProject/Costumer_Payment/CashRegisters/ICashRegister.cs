using PizzaProject.Costumer_Payment.People;
using PizzaProject.Dishes_Orders.Abstractions;
using PizzaProject.Dishes_Orders.Implementations;
using PizzaProject.Menu_Loyality.Menu;

namespace PizzaProject.Costumer_Payment.CashRegisters
{
    public delegate void OrderApperiance(Order order);

    public interface ICashRegister
    {
        IMenu Menu { get; }
        IEnumerable<Wallet.PaymentCategory> PaymentMethod { get; }
        int CustomersInQueue { get; }

        void AddToQueue(Customer customer);
        void DeleteFromQueue(Customer customer);

        void MakeOrder(Customer customer);

        bool AddDishToOrder(IOffer dish, List<Ingredient> additional); //можливо трохи інші параметри в залежності від організації меню

        void AddDishesToOrder(int[] dishId);
        bool Pay(Wallet customerWallet);

        event OrderApperiance NewOrderApperiance;
    }
}
