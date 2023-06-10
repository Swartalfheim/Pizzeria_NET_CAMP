using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Stubs
{
    internal class Manager : IStaff
    {
        private Menu _menu;

        public Manager(Guid id, string name, Menu menu)
        {
            Id = id;
            Name = name;
            Info = this.ToString();
            _menu = menu;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Info { get; }
    }
}
