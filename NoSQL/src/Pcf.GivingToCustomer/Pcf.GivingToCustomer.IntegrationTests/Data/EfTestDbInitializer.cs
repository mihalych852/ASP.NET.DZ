using Microsoft.EntityFrameworkCore;
using Pcf.GivingToCustomer.DataAccess;
using Pcf.GivingToCustomer.DataAccess.Data;

namespace Pcf.GivingToCustomer.IntegrationTests.Data
{
    public class EfTestDbInitializer
        : IDbInitializer
    {
        private readonly DbContext _dataContext;

        public EfTestDbInitializer(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();

            //_dataContext.AddRange(TestDataFactory.Preferences);
            //_dataContext.SaveChanges();
            
            _dataContext.AddRange(TestDataFactory.Customers);
            _dataContext.SaveChanges();
        }

        public void CleanDb()
        {
            _dataContext.Database.EnsureDeleted();
        }
    }
}