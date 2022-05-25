using Microsoft.EntityFrameworkCore;



public class DataContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Chat> chats { get; set; }
    public DbSet<Message> messages { get; set; }


    //private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=";
    public DataContext():base()
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Directory.GetCurrentDirectory();
        var DbPath = Path.Join(path, "mySqliteDB.db");
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Img>().Property(i => i.image);
        modelBuilder.Entity<User>().Property(u=>u.profileImg);*/
        modelBuilder.Entity<Message>().Property(e => e.MessageId).ValueGeneratedOnAdd();
    }

}

