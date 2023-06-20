using PizzaProject;

namespace Pizzaria_Library
{
    public class PizzariaLibrary
    {
        public Program pizzaApp;

        //IConnector connector
        public PizzariaLibrary(IConnector connector)
        {
            pizzaApp = new Program();
            pizzaApp.SetConnector(connector);
        }
    }
}