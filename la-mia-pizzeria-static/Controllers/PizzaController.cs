using Azure;
using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Database_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize(Roles = "USER,ADMIN")]
    public class PizzaController : Controller
    {

        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public PizzaController(PizzaContext db, ICustomLogger logger)
        {
            _myLogger = logger;
            _myDatabase = db;
        }

        
        public IActionResult Index()
        {
            _myLogger.WriteLog("L'utente è arrivato sulla pagina Pizza > Index");


            List<Pizza> pizzas = _myDatabase.Pizzas.Include(pizza => pizza.Category).Include(pizza => pizza.Ingredients).ToList<Pizza>();

            return View("Index", pizzas);

        }

       
        public IActionResult Details(int id)
        {
            Pizza? foundedElement = _myDatabase.Pizzas.Where(element => element.Id == id).Include(pizza => pizza.Category).Include(pizza => pizza.Ingredients).FirstOrDefault();

            if (foundedElement == null)
            {
                return NotFound($"La pizza con {id} non è stata trovata!");
            }
            else
            {
                return View("Details", foundedElement);
            }

        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDatabase.Categories.ToList();

            List<SelectListItem> allIngredientsSelectList = new List<SelectListItem>();
            List<Ingredient> databaseAllIngredients = _myDatabase.Ingredients.ToList();

            foreach (Ingredient ingredient in databaseAllIngredients)
            {
                allIngredientsSelectList.Add(
                    new SelectListItem
                    {
                        Text = ingredient.Title,
                        Value = ingredient.Id.ToString()
                    });
            }

            PizzaFormModel model =
                new PizzaFormModel { Pizza = new Pizza(), Categories = categories, Ingredients = allIngredientsSelectList };

            return View("Create", model);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel newPizza)
        {
            if (newPizza.Pizza.Image == null)
            {
                newPizza.Pizza.Image = "/img/default.jpg";
            }
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                newPizza.Categories = categories;

                List<SelectListItem> allIngredientsSelectList = new List<SelectListItem>();
                List<Ingredient> databaseAllIngredients = _myDatabase.Ingredients.ToList();

                foreach (Ingredient ingredient in databaseAllIngredients)
                {
                    allIngredientsSelectList.Add(
                        new SelectListItem
                        {
                            Text = ingredient.Title,
                            Value = ingredient.Id.ToString()
                        });
                }

                newPizza.Ingredients = allIngredientsSelectList;

                return View("Create", newPizza);
            }

            newPizza.Pizza.Ingredients = new List<Ingredient>();

            if (newPizza.SelectedIngredientsId != null)
            {
                foreach (string ingredientSelectedId in newPizza.SelectedIngredientsId)
                {
                    int intIngredientSelectedId = int.Parse(ingredientSelectedId);

                    Ingredient? ingredientInDb = _myDatabase.Ingredients.Where(ingredient => ingredient.Id == intIngredientSelectedId).FirstOrDefault();

                    if (ingredientInDb != null)
                    {
                        newPizza.Pizza.Ingredients.Add(ingredientInDb);
                    }
                }
            }


            _myDatabase.Pizzas.Add(newPizza.Pizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            
                Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Ingredients).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound("La pizza che vuoi modificare non è stata trovata");
                }
                else
                {
                    List<Category> categories = _myDatabase.Categories.ToList();

                    List<SelectListItem> selectListItem = new List<SelectListItem>();
                    List<Ingredient> dbingredientList = _myDatabase.Ingredients.ToList();

                    foreach (Ingredient ingredient in dbingredientList)
                    {
                        selectListItem.Add(new SelectListItem
                        {
                            Value = ingredient.Id.ToString(),
                            Text = ingredient.Title,
                            Selected = pizzaToEdit.Ingredients.Any(ingredAssociated => ingredAssociated.Id == ingredient.Id)
                        });

                    }

                    PizzaFormModel model
                      = new PizzaFormModel { Pizza = pizzaToEdit, Categories = categories, Ingredients = selectListItem };

                    return View("Update", model);   

                }
            
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;

                List<SelectListItem> selectListItem = new List<SelectListItem>();
                List<Ingredient> dbIngredientList = _myDatabase.Ingredients.ToList();

                foreach (Ingredient ingredient in dbIngredientList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Value = ingredient.Id.ToString(),
                        Text = ingredient.Title
                    });
                }

                data.Ingredients = selectListItem;


                return View("Update", data);
            }

                Pizza? pizzaToUpdate = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Ingredients).FirstOrDefault();

                if (pizzaToUpdate != null)
                {
                    pizzaToUpdate.Ingredients.Clear();

                    pizzaToUpdate.Name = data.Pizza.Name;
                    pizzaToUpdate.Description = data.Pizza.Description;
                    pizzaToUpdate.Price = data.Pizza.Price;
                    pizzaToUpdate.Image = data.Pizza.Image;
                    pizzaToUpdate.CategoryId = data.Pizza.CategoryId;


                if (data.SelectedIngredientsId != null)
                {
                    foreach (string ingredientSelectedId in data.SelectedIngredientsId)
                    {
                        int intIngredientSelectedId = int.Parse(ingredientSelectedId);

                        Ingredient? ingredientInDb = _myDatabase.Ingredients.Where(ingred => ingred.Id == intIngredientSelectedId).FirstOrDefault();

                        if (ingredientInDb != null)
                        {
                            pizzaToUpdate.Ingredients.Add(ingredientInDb);
                        }
                    }
                }

                _myDatabase.SaveChanges();

                    return RedirectToAction("Details", "Pizza", new { id = pizzaToUpdate.Id });
                }
                else
                {
                    return NotFound("Mi dispiace non è stata trovata la pizza da aggiornare");
                }        

        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
                Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    _myDatabase.Pizzas.Remove(pizzaToDelete);
                    _myDatabase.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La pizza da eliminare non è stata trovata!");
                }
            
        }

    }

}
