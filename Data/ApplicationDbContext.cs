using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Add DbSet for tasks
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        string fakeUserId = "2cdc1305-f21d-4f73-a1c2-f8ab15591172"; // Fake user ID

        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                TaskID = 1,
                Title = "Task 1",
                Description = "Description for Task 1",
                UserId = fakeUserId // Correct data type (string)
            },
            new TaskItem
            {
                TaskID = 2,
                Title = "Task 2",
                Description = "Description for Task 2",
                UserId = fakeUserId // Provide a valid UserId
            }
        );
    }
}
