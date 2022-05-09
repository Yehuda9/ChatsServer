using Microsoft.EntityFrameworkCore;



public class DataContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Chat> chats { get; set; }

    //public DbSet<ContactModel> contacts { get; set; }
    public DbSet<Message> messages { get; set; }

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
            entity.HasKey(c => new { c.idName }).HasName("id");
            entity.HasMany<ContactModel>();
        });
        /*modelBuilder.Entity<ContactModel>(entity =>
        {
            entity.HasKey(c => new { c.id,c.server }).HasName("id");
            entity.HasMany<Message>();
        });*/
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(c => new { c.id1, c.id2 });
            entity.HasMany<Message>();

        });
    }
}

