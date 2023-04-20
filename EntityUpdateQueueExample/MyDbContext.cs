using Microsoft.EntityFrameworkCore;

namespace EntityUpdateQueueExample
{
    public class MyDbContext : DbContext
    {
        public DbSet<MyEntity> MyEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-IHLCDJ5\\MSSQLSERVER01;Initial Catalog=EntityUpdateQueueTestDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
        }
    }
}
