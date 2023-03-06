using Microsoft.EntityFrameworkCore;

namespace WebApiMioExercise.Models
{
    public class ProductsDb : DbContext
    {
        public ProductsDb(DbContextOptions<ProductsDb> options)
            : base(options)
        {

        }

        public DbSet<Products> Products { get; set; } = null!;
    }
}
