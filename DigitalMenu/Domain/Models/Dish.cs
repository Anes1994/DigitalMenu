using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Domain.Models
{
    public class Dish
    {
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public List<string> ServingTime { get; set; }

        public List<string> AvailableDays { get; set; }

        public bool IsActive { get; set; }

        public int TimeToWaitInMinutes { get; set; }
    }
}
