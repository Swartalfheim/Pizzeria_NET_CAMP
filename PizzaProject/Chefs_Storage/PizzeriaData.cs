
using PizzaProject.Staff;

namespace PizzaProject
{
    public static class PizzeriaData
    {
        public static Storage Storage { get; set; } = new Storage();
        public static ChefManager ChefManager { get; set; } = new ChefManager("Muhammad");
    }
}
