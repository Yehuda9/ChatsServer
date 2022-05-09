using Microsoft.EntityFrameworkCore;



public class UserContext : DbContext
{
    private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=haim";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring the Name property as the primary
        // key of the Items table
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property<string>("Id").HasColumnName("id");
            entity.HasMany(e=>e.Com);
        });
        modelBuilder.Entity<User>().ToTable("users");
    }

    public DbSet<User> users { get; set; }
}

