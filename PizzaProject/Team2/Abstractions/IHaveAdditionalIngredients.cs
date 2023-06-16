﻿namespace PizzaProject.Team2.Abstractions
{
    public interface IHaveAdditionalIngredients
    {
        IEnumerable<IStorageable> AdditionalIngredients { get; }
        void AddAdditionalIngredients(List<IStorageable> ingredients);
    }
}