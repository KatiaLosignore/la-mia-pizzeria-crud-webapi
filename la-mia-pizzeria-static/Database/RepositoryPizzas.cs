using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

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
        public Pizza GetPizzaById(int id)
        {
            Pizza? pizza = _db.Pizzas.Where(pizza => pizza.Id == id)
                               .Include(pizza => pizza.Ingredients).Include(pizza => pizza.Category).FirstOrDefault();

            if (pizza != null)
            {
                return pizza;
            }
            else
            {
                throw new Exception("La pizza non è stata trovata!");
            }
        }

        public List<Pizza> GetPizzas()
        {
            List<Pizza> pizzas = _db.Pizzas.Include(pizza => pizza.Ingredients).ToList();
            return pizzas;
        }

        public List<Pizza> GetPizzasByTitle(string title)
        {
            List<Pizza> foundedPizzsd =
              _db.Pizzas.Where(pizza => pizza.Name.ToLower().Contains(title.ToLower())).ToList();

            return foundedPizzsd;
        }

        public bool AddPizza(Pizza pizzaToAdd)
        {
            try
            {
                _db.Pizzas.Add(pizzaToAdd);
                _db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModifyPizza(int id, Pizza updatedPizza)
        {
            Pizza? pizzaToUpdate = _db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Ingredients).FirstOrDefault();

            if (pizzaToUpdate != null)
            {
                pizzaToUpdate.Ingredients.Clear();

                pizzaToUpdate.Name = updatedPizza.Name;
                pizzaToUpdate.Description = updatedPizza.Description;
                pizzaToUpdate.Price = updatedPizza.Price;
                pizzaToUpdate.Image = updatedPizza.Image;
                pizzaToUpdate.CategoryId = updatedPizza.CategoryId;


                _db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeletePizza(int id)
        {

            Pizza? pizzaToDelete = _db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToDelete == null)
            {
                return false;
            }

            _db.Pizzas.Remove(pizzaToDelete);
            _db.SaveChanges();

            return true;

        }


    }

    
}