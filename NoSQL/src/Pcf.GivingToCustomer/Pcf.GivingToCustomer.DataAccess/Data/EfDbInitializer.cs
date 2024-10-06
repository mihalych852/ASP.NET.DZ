using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly DbContext _dataContext;

        public EfDbInitializer(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();

            //_dataContext.AddRange(FakeDataFactory.Preferences);
            //_dataContext.SaveChanges();
            
            _dataContext.AddRange(FakeDataFactory.Customers);
            _dataContext.SaveChanges();
        }
    }
}