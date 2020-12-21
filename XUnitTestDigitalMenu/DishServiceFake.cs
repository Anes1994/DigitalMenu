using DigitalMenu.Domain.Models;
using DigitalMenu.Domain.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestDigitalMenu
{
    class DishServiceFake : IDishService
    {
        private readonly List<Dish> _dishes;

        public DishServiceFake()
        {
            _dishes = new List<Dish>()
            {
                new Dish()
                {
                    Id = "5fdfa910cf6e8d7bd407d4b9",
                    Name = "Red and White Tortellini",
                    Description = "This fabulous recipe uses all the tricks of a seasoned cook who knows what it's like to be pressed for time. Buy the frozen ravioli of your choice and one jar of red sauce (no added sugar) and one of Alfredo sauce.",
                    Price = 15,
                    Category = "Main Course",
                    ServingTime = new List<string>(){"Lunch", "Dinner"},
                    AvailableDays = new List<string>(){"Monday","Tuesday","Wednesday","Thursday"},
                    IsActive = true,
                    TimeToWaitInMinutes = 20
                },
                new Dish()
                {
                    Id = "5fdfa9cbcf6e8d7bd407d4ba",
                    Name = "Mojito",
                    Description = "Mojito is a traditional Cuban highball. The cocktail often consists of five ingredients: white rum, sugar (traditionally sugar cane juice), lime juice, soda water, and mint.",
                    Price = 5,
                    Category = "Beverage",
                    ServingTime = new List<string>(){"BreakFast","Lunch", "Dinner"},
                    AvailableDays = new List<string>(){"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"},
                    IsActive = true,
                    TimeToWaitInMinutes = 10
                },
                new Dish()
                {
                    Id = "5fdfab29ea04c7f3731fcdeb",
                    Name = "Stew",
                    Description = "A stew is a combination of solid food ingredients that have been cooked in liquid and served in the resultant gravy.",
                    Price = 7,
                    Category = "Starter",
                    ServingTime = new List<string>(){"Lunch"},
                    AvailableDays = new List<string>(){"Saturday","Sunday"},
                    IsActive = true,
                    TimeToWaitInMinutes = 5
                }
            };
        }
        public async Task<Dish> AddDishAsync(Dish dish)
        {
            dish.Id = ObjectId.GenerateNewId().ToString();
            _dishes.Add(dish);
            await Task.Yield();
            return dish;
        }

        public async Task DeleteDishAsync(Dish dishIn)
        {
            await Task.Yield();
            var dish = _dishes.First(d => d.Id == dishIn.Id);
            _dishes.Remove(dish);
        }

        public async Task DeleteDishByIdAsync(string id)
        {
            await Task.Yield();
            var dish = _dishes.First(d => d.Id == id);
            _dishes.Remove(dish);
        }

        public async Task<Dish> GetDishByIdAsync(string id)
        {
            await Task.Yield();
            var dish = _dishes.Where(d => d.Id == id).FirstOrDefault();
            return dish;
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            await Task.Yield();
            return _dishes;
        }

        public async Task UpdateDishByIdAsync(string id, Dish dishIn)
        {
            await Task.Yield();
            int dishIndex = _dishes.FindIndex(dish => dish.Id == id);
            _dishes[dishIndex] = dishIn;
        }
    }
}
