﻿using Microsoft.EntityFrameworkCore;



public class DataContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Chat> chats { get; set; }
    public DbSet<Message> messages { get; set; }


    private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=haim";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var DbPath = Path.Join(path, "mySqliteDB.db");
        DbPath = "C:\\Users\\yehud\\Desktop\\ChatsServer\\mySqliteDB.db";*/
        string path = Directory.GetCurrentDirectory();
        var DbPath = Path.Join(path, "mySqliteDB.db");

        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        //optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().Property(e => e.MessageId).ValueGeneratedOnAdd();
        // Configuring the Name property as the primary
        // key of the Items table
        /*modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.idName);
            //entity.HasMany(e=>e.Com);
        });*/
        //modelBuilder.Entity<User>().HasMany(u => u.message);
        //modelBuilder.Entity<User>().HasMany<Contact>().WithMany(u=>u.userId).HasForeignKey(x => x.ContactId);

        /*modelBuilder.Entity<Message>().HasKey(x => new { x.fromId, x.toId });
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Message>().ToTable("contact");
        modelBuilder.Entity<Message>().ToTable("message");*/
        /*modelBuilder.Entity<UserMessage>()
            .HasOne(u => u.message)
            .WithMany(um => um.userMessages)
            .HasForeignKey(ui => ui.messageId);*/
    }

}

