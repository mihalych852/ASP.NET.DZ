using Microsoft.EntityFrameworkCore;
using Pcf.GivingToCustomer.Core.Domain;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Pcf.GivingToCustomer.DataAccess.Infrastructure;

namespace Pcf.GivingToCustomer.DataAccess
{
    public class MongoDBContext : DbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;
        private readonly string _dbName;

        public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _client = new MongoClient(mongoDBSettings.Value.Connection);
            _dbName = mongoDBSettings.Value.DatabaseName;
            _database = _client.GetDatabase(_dbName);
        }

        public IMongoDatabase Database => _database;

        //private readonly MongoClient _client;

        //public MongoDBContext(MongoClient client)
        //{
        //    _client = client;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMongoDB(_client, _dbName);

        public DbSet<Customer> Customers => Set<Customer>();
        // Иначе про PromocodesController можно забыть
        // , т.к. PromoCode тогда получается настроен как принадлежащий объект
        // , и доступ к нему должен осуществляться через принадлежащий объект типа "Customer"
        public DbSet<PromoCode> PromoCodes => Set<PromoCode>(); 
    }
}
