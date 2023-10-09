using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Database
{
    public class RepositoryPizzas : IRepositoryPizzas
    {
        // uso la  Dependency Injection

        private PizzaContext _db;

        public RepositoryPizzas(PizzaContext db)
        {
            _db = db;
        }

        // implemento i Metodi creati nell' Interfaccia
        public bool AddPizza(Pizza pizzaToAdd)
        {
            throw new NotImplementedException();
        }

        public bool DeletePizza(int id)
        {
            throw new NotImplementedException();
        }

        public Pizza GetPizzaById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pizza> GetPizzas()
        {
            throw new NotImplementedException();
        }

        public List<Pizza> GetPizzasByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public bool ModifyPizza(int id, Pizza updatedPizza)
        {
            throw new NotImplementedException();
        }
    }
}
