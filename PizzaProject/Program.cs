using PizzaProject.Staff;

namespace PizzaProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            var pizzaIngredients = new Dictionary<Ingredient, uint>()
            {
                {new Ingredient{Name = "Cheese"} , 2 },
                { new Ingredient{Name = "Tomato"} , 2 },
                { new Ingredient { Name = "Dough" }, 1 }
            };
            var pizzaRecipe = new Recipe("Pizza", pizzaIngredients);

            var cakeIngredients = new Dictionary<Ingredient, uint>()
            {
                { new Ingredient { Name = "Cheese" }, 1 },
                { new Ingredient { Name = "Milk" }, 1 }
            };
            var cakeRecipe = new Recipe("Cake", cakeIngredients);


            // Add chefs
            /*Chef chef1 = new Chef("Linda", new Dictionary<string, Recipe>
                                           {
                                               { pizzaRecipe.Name, pizzaRecipe },
                                               { cakeRecipe.Name, cakeRecipe }
                                           }
            );
            chef1.DishPrepared += Print;
            PizzeriaData.ChefManager.Chefs.Add(chef1);*/

            Chef chef2 = new Chef("Abdulla", new Dictionary<string, Recipe>
                                          {
                                              { pizzaRecipe.Name, pizzaRecipe },
                                              { cakeRecipe.Name, cakeRecipe },
                                          }
            );
            chef2.DishPrepared += Print;
            PizzeriaData.ChefManager.Chefs.Add(chef2);

            Chef chef3 = new Chef("Marichka", new Dictionary<string, Recipe>
                                          {
                                              { cakeRecipe.Name, cakeRecipe }
                                          }
            );
            chef3.DishPrepared += Print;
            PizzeriaData.ChefManager.Chefs.Add(chef3);



            // SIMULATION ---------

            Console.WriteLine(PizzeriaData.Storage.ToString());
            Console.WriteLine(new string('-', 20));

            PizzeriaData.ChefManager.AddOrder("Pizza");
            PizzeriaData.ChefManager.AddOrder("Cake");

            Console.ReadKey();

            PizzeriaData.ChefManager.AddOrder("Pizza");
            PizzeriaData.ChefManager.AddOrder("Cake");
            PizzeriaData.ChefManager.AddOrder("Cake");
            PizzeriaData.ChefManager.AddOrder("Cake");


            Console.ReadKey();
            PizzeriaData.Storage.PutIngredient( new Ingredient() { Name = "Cheese" }, 100);
            PizzeriaData.Storage.PutIngredient(new Ingredient() { Name = "Milk" }, 100);

            Console.ReadKey();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine(PizzeriaData.Storage.ToString());
        }

        public static void Print(string name, string dishName)
        {
            Console.WriteLine($"Chef {name} has cooked {dishName}");
        }
    }
}