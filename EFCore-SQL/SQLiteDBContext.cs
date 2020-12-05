using Microsoft.EntityFrameworkCore;
using EFCore_SQL.Properties;
using System.Configuration;

namespace EFCore_SQL
{
    public class SQLiteDBContext : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Appsettings> Appsettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(appconfig.ConnectionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define composite key.
            builder.Entity<Appsettings>()
                .HasNoKey();
        }
    }
}
