
namespace PizzaProject
{
    public class UniqueIntGenerator
    {
        private static uint currentValue = 0;

        public static uint GetUniqueInt()
        {
            return Interlocked.Increment(ref currentValue);
        }
    }
}
