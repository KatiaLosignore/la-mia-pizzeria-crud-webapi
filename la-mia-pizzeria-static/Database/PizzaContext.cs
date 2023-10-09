using Azure;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Database_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Database
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MyPizzeriaNew2023;Integrated Security=True;TrustServerCertificate=True");
        }


    }
}
