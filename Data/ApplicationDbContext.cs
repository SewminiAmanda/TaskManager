using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet for tasks
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    TaskID = 1,
                    Title = "Task 1",
                    Description = "Description for Task 1"
                },
                new TaskItem
                {
                    TaskID = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2"
                }
            );
        }
    }
}
