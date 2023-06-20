using Pizzaria_Library;

namespace PizzaClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Connector connector = new Connector();
            Console.WriteLine("Hello, World!");
            PizzariaLibrary libr = new PizzariaLibrary(connector);

            libr.pizzaApp.PizzaInit();

            Thread pizza = new Thread(libr.pizzaApp.Start);

            pizza.Start();


            //libr.pizzaApp.Start();


            libr.pizzaApp.SetChefInAdmin("Barbarus Name lok!!!!!!!!!!!!!!!!!!!!!!!!!!", new int[] {0, 1, 2});


            string res = libr.pizzaApp.GetMenu();

            Console.WriteLine(res);

            string res2 = libr.pizzaApp.GetIngredients();

            Console.WriteLine(res2);


            libr.pizzaApp.AddClientAdmin("Long Name Client", 1, 8000, 1);

            libr.pizzaApp.AddClient("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa Customer", 8000, 1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });

            libr.pizzaApp.AddWaiter("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq Waiter");

            libr.pizzaApp.AddDishInMenu("Gawain Pizza long name", "Manager new dish", new int[] {0, 1, 2}, new uint[] {5, 5, 5}, 3, 3, 2, 400);

            libr.pizzaApp.AddClient("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb Customer", 8000, 1, new int[] { 3, 3, 3, 3 });

            Console.ReadLine();
        }
    }
}