﻿using PizzaProject.Team1.TemporaryClasses;
using PizzaProject.Team1.People;
namespace PizzaProject.Team1.CashRegisters
{
    public delegate void OrderApperiance(object order);
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