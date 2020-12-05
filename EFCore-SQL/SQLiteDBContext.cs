using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EFCore_SQL
{
    public class SQLiteDBContext : DbContext
    {

        public static readonly string DateFormatConfProp = ConfigurationManager.AppSettings["DBConnectionString"].Trim();

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Appsettings> Appsettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=sqlitedemo.db");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define composite key.
            builder.Entity<Appsettings>()
                .HasNoKey();
        }
    }
}
