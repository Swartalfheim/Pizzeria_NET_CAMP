namespace PizzaProject
{
    public class Ingredient
    {
        public string Name { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj is not Ingredient)
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}