using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Team1.TemporaryClasses
{
    internal class Wallet
    {
        public MoneyCategory Category { get; set; }
        public enum MoneyCategory
        {
            Cashe,
            paymentService,
            Card
        }
    }
}
