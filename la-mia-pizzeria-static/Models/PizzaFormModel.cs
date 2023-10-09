using la_mia_pizzeria_static.Models.Database_Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }


        // Inserisco nuove proprietà per gestire una select multipla nelle viste
        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredientsId { get; set; }
    }
}
