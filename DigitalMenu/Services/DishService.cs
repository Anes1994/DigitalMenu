using DigitalMenu.Domain.Models;
using DigitalMenu.Domain.Repositories;
using DigitalMenu.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<Dish> GetDishByIdAsync(string id)
        {
            return await _dishRepository.GetDishByIdAsync(id);       
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await _dishRepository.GetDishesAsync();
        }

        public async Task<Dish> AddDishAsync(Dish dish)
        {
            return await _dishRepository.AddDishAsync(dish);
        }

        public async Task UpdateDishByIdAsync(string id, Dish dishIn)
        {
            await _dishRepository.UpdateDishByIdAsync(id, dishIn);
        }

        public async Task DeleteDishByIdAsync(string id)
        {
            await _dishRepository.DeleteDishByIdAsync(id);
        }

        public async Task DeleteDishAsync(Dish dishIn)
        {
            await _dishRepository.DeleteDishAsync(dishIn);
        }
    }
}
