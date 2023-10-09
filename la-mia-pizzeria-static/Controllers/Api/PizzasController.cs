﻿using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        // uso la  Dependency Injection

        private IRepositoryPizzas _repoPizzas;

        public PizzasController(IRepositoryPizzas repoPizzas)
        {
            _repoPizzas = repoPizzas;
        }


        [HttpGet]
        public IActionResult GetPizzas()
        {

            List<Pizza> pizzas = _repoPizzas.GetPizzas();

            return Ok(pizzas);

        }


        [HttpGet]
        public IActionResult SearchArticles(string? search)
        {
            if (search == null)
            {
                return BadRequest(new { Message = "Non hai inserito nessuna stringa di ricerca" });
            }


            List<Pizza> foundedPizzas = _repoPizzas.GetPizzasByTitle(search);

            return Ok(foundedPizzas);

        }

        [HttpGet("{id}")]
        public IActionResult PizzaById(int id)
        {
            Pizza pizza = _repoPizzas.GetPizzaById(id);

            if (pizza != null)
            {
                return Ok(pizza);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody] Pizza newPizza)
        {
            try
            {
                bool result = _repoPizzas.AddPizza(newPizza);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Modify(int id, [FromBody] Pizza updatedPizza)
        {
            bool result = _repoPizzas.ModifyPizza(id, updatedPizza);

            
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _repoPizzas.DeletePizza(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



    }


}
