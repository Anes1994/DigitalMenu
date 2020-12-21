using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Resources
{
    public class SaveDishResource
    {
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
