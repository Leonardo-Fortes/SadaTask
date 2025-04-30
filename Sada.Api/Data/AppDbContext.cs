using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Sada.Core.Entities;


namespace Sada.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskSada> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>().HasNoKey();
        }
    }
}
