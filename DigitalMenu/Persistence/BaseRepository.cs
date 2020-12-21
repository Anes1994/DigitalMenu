using DigitalMenu.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Persistence
{
    public abstract class BaseRepository
    {
        protected IMongoDatabase database;
        public BaseRepository(IDigitalMenuDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
