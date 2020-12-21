using DigitalMenu.Configuration;
using DigitalMenu.Domain.Models;
using DigitalMenu.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Persistence.Repositories
{
    public class DishRepository : BaseRepository, IDishRepository
    {
        private readonly IMongoCollection<Dish> Dishes;
        public DishRepository(IDigitalMenuDatabaseSettings settings) : base(settings)
        {
            Dishes = database.GetCollection<Dish>(settings.DishesCollectionName);
        }

        public async Task<Dish> GetDishByIdAsync(string id)
        {
            return await Dishes.Find(dish => dish.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await Dishes.Find(dish => true).ToListAsync();
        }

        public async Task<Dish> AddDishAsync(Dish dish)
        {
            await Dishes.InsertOneAsync(dish);
            return dish;
        }

        public async Task UpdateDishByIdAsync(string id, Dish dishIn)
        {
            await Dishes.ReplaceOneAsync(dish => dish.Id == id, dishIn);
        }

        public async Task DeleteDishByIdAsync(string id)
        {
            await Dishes.DeleteOneAsync(dish => dish.Id == id);
        }

        public async Task DeleteDishAsync(Dish dishIn)
        {
            await Dishes.DeleteOneAsync(dish => dish.Id == dishIn.Id);
        }
    }
}
