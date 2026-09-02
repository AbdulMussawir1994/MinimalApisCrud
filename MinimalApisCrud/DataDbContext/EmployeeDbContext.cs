using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MinimalApisCrud.Entity.Model;

namespace MinimalApisCrud.DataDbContext;

public class EmployeeDbContext : DbContext
{

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if (databaseCreator is not null)
            {
                if (!databaseCreator.CanConnect())
                {
                    databaseCreator.Create();
                }

                if (!databaseCreator.HasTables())
                {
                    databaseCreator.CreateTables();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Some thing issue in database creating", ex.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().ToTable("Employee", "dbo");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Employee> Employees => Set<Employee>();
}
