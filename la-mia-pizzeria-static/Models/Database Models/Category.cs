using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models.Database_Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo della categoria è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Il titolo della categoria non può superare i 50 caratteri")]
        public string Title { get; set; }


        // Creo la relazione 1:N con la classe Pizza
        public List<Pizza>? Pizzas { get; set; }


        public Category()
        {

        }
    }

}