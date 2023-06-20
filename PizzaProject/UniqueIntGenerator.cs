
namespace PizzaProject
{
    public class UniqueIntGenerator
    {
        private static uint currentOrderValue = 0;
        private static uint currentCustomerValue = 0;
        private static uint currentWaiterValue = 0;
        private static uint currentChefValue = 0;


        public static uint GetUniqueOrderInt()
        {
            return Interlocked.Increment(ref currentOrderValue);
        }

        public static uint GetUniqueCustomerInt()
        {
            return Interlocked.Increment(ref currentCustomerValue);
        }

        public static uint GetUniqueWaiterInt()
        {
            return Interlocked.Increment(ref currentWaiterValue);
        }

        public static uint GetUniqueChefInt()
        {
            return Interlocked.Increment(ref currentChefValue);
        }
    }
}
