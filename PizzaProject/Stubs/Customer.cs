using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Stubs
{
    internal class Customer : IPerson
    {
        private HashSet<VipLvl> _vipStatus;

        public Customer()
        {
            _vipStatus = new HashSet<VipLvl>() { VipLvl.None };
        }

        public Guid Id { get; }
        public string Name { get; }
        public HashSet<VipLvl> VipStatus { get => _vipStatus; }
    }
}
