﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Stubs
{
    internal class ChefManager : IStaff
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Info { get; }
    }
}