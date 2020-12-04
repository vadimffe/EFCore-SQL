using Microsoft.EntityFrameworkCore;

namespace EFCore_SQL
{
    public class SQLiteDBContext : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=sqlitedemo.db");
    }
}
