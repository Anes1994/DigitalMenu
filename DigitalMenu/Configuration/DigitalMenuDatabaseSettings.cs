using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Configuration
{
    public class DigitalMenuDatabaseSettings : IDigitalMenuDatabaseSettings
    {
        public string DishesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
