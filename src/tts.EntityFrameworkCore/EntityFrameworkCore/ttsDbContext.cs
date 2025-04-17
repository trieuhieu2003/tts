using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using tts.Authorization.Roles;
using tts.Authorization.Users;
using tts.MultiTenancy;
using tts.Categories;
using tts.Products;

namespace tts.EntityFrameworkCore
{
    public class ttsDbContext : AbpZeroDbContext<Tenant, Role, User, ttsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ttsDbContext(DbContextOptions<ttsDbContext> options)
            : base(options)
        {
        }
    }
}
