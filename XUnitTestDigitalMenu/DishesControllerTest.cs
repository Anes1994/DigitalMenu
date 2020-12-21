using AutoMapper;
using DigitalMenu.Controllers;
using DigitalMenu.Domain.Models;
using DigitalMenu.Domain.Services;
using DigitalMenu.Resources;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestDigitalMenu
{
    public class DishesControllerTest
    {
        DishesController _controller;
        IDishService _service;
        IMapper _mapper;
        public DishesControllerTest()
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<SaveDishResource, Dish>()
                );
            _mapper = new Mapper(config);

            _service = new DishServiceFake();
            _controller = new DishesController(_service,_mapper);
        }


        // Testing the Get Method
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResponse = await _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResponse = await _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Dish>>(okResponse.Value);
            Assert.Equal(3, items.Count);
        }


        // Testing the GetById method
        [Fact]
        public async Task GetById_noxExistingObjectIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingObjectId = ObjectId.GenerateNewId().ToString();

            // Act
            var badResponse = await _controller.Get(nonExistingObjectId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async Task GetById_ExistingObjectIdPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = ObjectId.Parse("5fdfa910cf6e8d7bd407d4b9").ToString();

            // Act
            var okResponse = await _controller.Get(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task GetById_ExistingObjectIdPassed_ReturnsRightItem()
        {
            // Arrange
            var existingId = ObjectId.Parse("5fdfa910cf6e8d7bd407d4b9").ToString();

            // Act
            var okResponse = await _controller.Get(existingId) as OkObjectResult;

            // Assert
            Assert.IsType<Dish>(okResponse.Value);
            Assert.Equal(existingId, (okResponse.Value as Dish).Id);
        }


        // Testing the Add Method
        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            SaveDishResource testItem = new SaveDishResource()
            {
                Name = "Coca Cola",
                Description = "Coca Cola is a carbonated soft drink manufactured by The Coca-Cola Company. ",
                Price = 3,
                Category = "Beverage",
                ServingTime = new List<string>() {"Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                IsActive = true,
                TimeToWaitInMinutes = 2
            };

            // Act
            var createdResponse = await _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(createdResponse);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new SaveDishResource()
            {
                Name = "Coca Cola",
                Description = "Coca Cola is a carbonated soft drink manufactured by The Coca-Cola Company. ",
                Price = 3,
                Category = "Beverage",
                ServingTime = new List<string>() { "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                IsActive = true,
                TimeToWaitInMinutes = 2
            };

            // Act
            var createdResponse = await _controller.Post(testItem) as CreatedAtRouteResult;
            var item = createdResponse.Value as Dish;

            // Assert
            Assert.IsType<Dish>(item);
            Assert.Equal("Coca Cola", item.Name);
        }


        // Testing the DeleteById method
        [Fact]
        public async Task Delete_NotExistingObjectIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingObjectId = ObjectId.GenerateNewId().ToString();

            // Act
            var badResponse = await _controller.Delete(notExistingObjectId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async Task Delete_ExistingObjectIdPassed_ReturnsOkResult()
        {
            // Arrange
            var existingObjectId = ObjectId.Parse("5fdfa910cf6e8d7bd407d4b9").ToString();

            // Act
            var okResponse = await _controller.Delete(existingObjectId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }
        [Fact]
        public async Task Delete_ExistingObjectIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingObjectId = ObjectId.Parse("5fdfa910cf6e8d7bd407d4b9").ToString();

            // Act
            await _controller.Delete(existingObjectId);
            var restElements = await _service.GetDishesAsync();

            // Assert
            Assert.Equal(2, restElements.Count);
        }


        // Testing the DeleteByObject method
        [Fact]
        public async Task Delete_NonExistingObjectPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var nonExistingObject = new Dish()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Coca Cola",
                Description = "Coca Cola is a carbonated soft drink manufactured by The Coca-Cola Company. ",
                Price = 3,
                Category = "Beverage",
                ServingTime = new List<string>() { "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                IsActive = true,
                TimeToWaitInMinutes = 2
            };

            // Act
            var badResponse = await _controller.Delete(nonExistingObject);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async Task Delete_ExistingObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var existingObject = new Dish()
            {
                Id = "5fdfa910cf6e8d7bd407d4b9",
                Name = "Red and White Tortellini",
                Description = "This fabulous recipe uses all the tricks of a seasoned cook who knows what it's like to be pressed for time. Buy the frozen ravioli of your choice and one jar of red sauce (no added sugar) and one of Alfredo sauce.",
                Price = 15,
                Category = "Main Course",
                ServingTime = new List<string>() { "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday" },
                IsActive = true,
                TimeToWaitInMinutes = 20
            };

            // Act
            var okResponse = await _controller.Delete(existingObject);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task Delete_ExistingObjectPassed_RemovesOneItem()
        {
            // Arrange
            var existingObject = new Dish()
            {
                Id = "5fdfa910cf6e8d7bd407d4b9",
                Name = "Red and White Tortellini",
                Description = "This fabulous recipe uses all the tricks of a seasoned cook who knows what it's like to be pressed for time. Buy the frozen ravioli of your choice and one jar of red sauce (no added sugar) and one of Alfredo sauce.",
                Price = 15,
                Category = "Main Course",
                ServingTime = new List<string>() { "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday" },
                IsActive = true,
                TimeToWaitInMinutes = 20
            };

            // Act
            await _controller.Delete(existingObject);
            var restElements = await _service.GetDishesAsync();

            // Assert
            Assert.Equal(2, restElements.Count);
        }

        
        // Testing the Put method
        [Fact]
        public async Task Put_NonExistingObjectIdPassed_ExistingObjectModifiedPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingObjectId = ObjectId.GenerateNewId().ToString();

            var existingObjectModified = new Dish()
            {
                Id = "5fdfa9cbcf6e8d7bd407d4ba",
                Name = "MojitoV2",
                Description = "Mojito is a traditional Cuban highball. The cocktail often consists of five ingredients: white rum, sugar (traditionally sugar cane juice), lime juice, soda water, and mint.",
                Price = 5,
                Category = "Beverage",
                ServingTime = new List<string>() { "BreakFast", "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                IsActive = true,
                TimeToWaitInMinutes = 10
            };

            // Act
            var badResponse = await _controller.Put(notExistingObjectId,existingObjectModified);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async Task Put_ExistingObjectIdPassed_ExistingObjectModifiedPassed_ReturnsOkResult()
        {
            // Arrange
            var notExistingObjectId = ObjectId.Parse("5fdfa9cbcf6e8d7bd407d4ba").ToString();

            var existingObjectModified = new Dish()
            {
                Id = "5fdfa9cbcf6e8d7bd407d4ba",
                Name = "MojitoV2",
                Description = "Mojito is a traditional Cuban highball. The cocktail often consists of five ingredients: white rum, sugar (traditionally sugar cane juice), lime juice, soda water, and mint.",
                Price = 5,
                Category = "Beverage",
                ServingTime = new List<string>() { "BreakFast", "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                IsActive = true,
                TimeToWaitInMinutes = 10
            };

            // Act
            var okResponse = await _controller.Put(notExistingObjectId, existingObjectModified);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task Put_ExistingObjectIdPassed_ExistingObjectModifiedPassed_ReturnsModifiedElement()
        {
            // Arrange
            var existingObjectId = ObjectId.Parse("5fdfa9cbcf6e8d7bd407d4ba").ToString();

            var existingObjectModified = new Dish()
            {
                Id = "5fdfa9cbcf6e8d7bd407d4ba",
                Name = "MojitoV2",
                Description = "Mojito is a traditional Cuban highball. The cocktail often consists of five ingredients: white rum, sugar (traditionally sugar cane juice), lime juice, soda water, and mint.",
                Price = 5,
                Category = "Beverage",
                ServingTime = new List<string>() { "BreakFast", "Lunch", "Dinner" },
                AvailableDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                IsActive = true,
                TimeToWaitInMinutes = 10
            };

            // Act
            var okResponse = await _controller.Put(existingObjectId, existingObjectModified) as OkObjectResult;

            // Assert
            Assert.Equal("MojitoV2", (okResponse.Value as Dish).Name);
        }
    }
}
