using Azure;
using la_mia_pizzeria_static.Models.Database_Models;
using la_mia_pizzeria_static.Nuova_cartella3;
using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della pizza è obbligatorio!")]
        [MaxLength(150, ErrorMessage = "La massima lunghezza del nome è di 150 caratteri")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "La descrizione della pizza è obbligatoria!")]
        [MoreThanFiveWords]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il prezzo della pizza è obbligatorio!")]
        [Range(1, 100, ErrorMessage = "Il prezzo di una pizza deve variare da € 1 a € 100")]
        public float Price { get; set; }

        [MaxLength(500, ErrorMessage = "La massima lunghezza del link è di 500 caratteri")]
        [Required(ErrorMessage = "L'inserimento dell'immagine è obbligatoria!")]
        [ValidImagePath(ErrorMessage = "Percorso immagine non valido.")]
        public string Image { get; set; }



        // Creo la relazione 1:N con la classe Category
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        // creo la relazione N a N con la classe Ingredient
        public List<Ingredient>? Ingredients { get; set; }



        public Pizza() { }
        
        public Pizza(string name, string description, float price, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Image = image;
        }


    }
}
