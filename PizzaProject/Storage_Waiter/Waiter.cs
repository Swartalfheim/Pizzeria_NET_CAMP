using PizzaProject.Storage_Waiter.Interfaces;

namespace PizzaProject
{
    public class Waiter : IStaff
    {
        private string _name;
        public string Info => _name;
    }
}