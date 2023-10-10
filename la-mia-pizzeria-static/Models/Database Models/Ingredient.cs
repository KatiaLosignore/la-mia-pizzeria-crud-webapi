using System.Text.Json.Serialization;

namespace la_mia_pizzeria_static.Models.Database_Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Title { get; set; }


        // creo la relazione N a N con la classe Pizza
        [JsonIgnore]
        public List<Pizza> Pizzas { get; set; }

        public Ingredient()
        {

        }
    }
}
