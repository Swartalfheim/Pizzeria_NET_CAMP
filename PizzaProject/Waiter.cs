using PizzaProject.Interfaces;

namespace PizzaProject
{
    public class Waiter : IStaff
    {
        private string _name;
        public string Info => _name;
    }
}