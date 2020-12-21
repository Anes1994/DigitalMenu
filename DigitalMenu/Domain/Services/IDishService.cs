using DigitalMenu.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Domain.Services
{
    public interface IDishService
    {
        Task<Dish> GetDishByIdAsync(string id);
        Task<List<Dish>> GetDishesAsync();
        Task<Dish> AddDishAsync(Dish dish);
        Task UpdateDishByIdAsync(string id, Dish dishIn);
        Task DeleteDishByIdAsync(string id);
        Task DeleteDishAsync(Dish dishIn);
    }
}
