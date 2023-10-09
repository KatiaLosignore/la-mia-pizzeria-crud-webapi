﻿using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Database
{
    public interface IRepositoryPizzas
    {
        public Pizza GetPizzaById(int id);
        public List<Pizza> GetPizzas();
        public List<Pizza> GetPizzasByTitle(string title);
        public bool AddPizza(Pizza pizzaToAdd);
        public bool ModifyPizza(int id, Pizza updatedPizza);
        public bool DeletePizza(int id);

    }
}