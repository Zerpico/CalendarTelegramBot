using Microsoft.EntityFrameworkCore;

namespace CalendarTelegramBot.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DbLog> DbLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=dblog.db");
        }
    }
}