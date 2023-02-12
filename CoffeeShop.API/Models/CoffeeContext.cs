using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeShop.API.Models
{
    public class CoffeeContext:DbContext
    {
     public   CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options)
        {

        }
        public DbSet<CustOrders> CustOrders { get; set; }


        public class YourDbContextFactory : IDesignTimeDbContextFactory<CoffeeContext>
        {
            public CoffeeContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CoffeeContext>();
                optionsBuilder.UseSqlServer("Server=DESKTOP-1IC1TU6;Initial Catalog=CoffeeContext;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False;");

                return new CoffeeContext(optionsBuilder.Options);
            }
        }


       


    }
}
