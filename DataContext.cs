using Microsoft.EntityFrameworkCore;



public class DataContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Contact> contacts { get; set; }

    private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=haim";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring the Name property as the primary
        // key of the Items table
        /*modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.idName);
            //entity.HasMany(e=>e.Com);
        });*/
        //modelBuilder.Entity<User>().HasMany<Contact>().WithMany(u=>u.userId).HasForeignKey(x => x.ContactId);
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Contact>().ToTable("contact");
        modelBuilder.Entity<Message>().ToTable("message");
        modelBuilder.Entity<User>().HasKey(e => e.idName);
        modelBuilder.Entity<Contact>().HasKey(e => new {e.id,e.server});
        modelBuilder.Entity<Message>().HasKey(e => e.id);
    }

}

