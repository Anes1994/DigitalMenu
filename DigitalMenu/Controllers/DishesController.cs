using AutoMapper;
using DigitalMenu.Domain.Models;
using DigitalMenu.Domain.Services;
using DigitalMenu.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IDishService DishService { get; set; }
        private IMapper Mapper { get; set; }
        public DishesController(IDishService dishService, IMapper mapper)
        {
            DishService = dishService;
            Mapper = mapper;
        }


        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IList<Dish> dishes = await DishService.GetDishesAsync();

            return Ok(dishes);
        }


        // GET api/Dishes/5fdf483965477534b52d3f4e
        [HttpGet("{id:length(24)}", Name ="GetDish")]
        public async Task<ActionResult> Get(string id)
        {
            Dish dish = await DishService.GetDishByIdAsync(id);

            if(dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }


        // POST api/Dishes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SaveDishResource resource)
        {
            var dish = Mapper.Map<SaveDishResource, Dish>(resource);
            
            Dish addedDish = await DishService.AddDishAsync(dish);
            return CreatedAtRoute("GetDish", new { id = addedDish.Id }, addedDish);
        }


        // PUT api/Dishes/5fdf483965477534b52d3f4e
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, [FromBody] Dish dishIn)
        {
            Dish dish = await DishService.GetDishByIdAsync(id);

            if(dish == null)
            {
                return NotFound();
            }

            await DishService.UpdateDishByIdAsync(id, dishIn);

            return Ok(dishIn);
        }


        // DELETE api/Dishes/5fdf483965477534b52d3f4e
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            Dish dish = await DishService.GetDishByIdAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            await DishService.DeleteDishByIdAsync(id);

            return Ok(dish);
        }


        // DELETE api/Dishes
        [HttpDelete()]
        public async Task<ActionResult> Delete(Dish dishIn)
        {
            Dish dish = await DishService.GetDishByIdAsync(dishIn.Id);

            if (dish == null)
            {
                return NotFound();
            }

            await DishService.DeleteDishAsync(dishIn);

            return Ok(dishIn);
        }
    }
}
