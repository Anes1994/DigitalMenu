using AutoMapper;
using DigitalMenu.Domain.Models;
using DigitalMenu.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Mapping
{
    public class DishMapper : Profile
    {
        public DishMapper()
        {
            CreateMap<SaveDishResource, Dish>();
        }
    }
}
