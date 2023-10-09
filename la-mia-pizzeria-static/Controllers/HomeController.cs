using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {
        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public HomeController(PizzaContext db, ICustomLogger logger)
        {
            _myLogger = logger;
            _myDatabase = db;
        }


        public IActionResult Index()
        {
            _myLogger.WriteLog("L'utente è arrivato sulla pagina Home > Index");

            List<Pizza> pizzas = _myDatabase.Pizzas.Include(pizza => pizza.Category).ToList<Pizza>();

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


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}