using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TechSupport.Data_Access_Layer
{
    public class CommonContext : DbContext
    {
        private readonly string _connectionString;

        public CommonContext(IOptions<CommonContextOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Task>();
            modelBuilder.Entity<Models.User>();
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.User> Users { get; set; }
    }
}
