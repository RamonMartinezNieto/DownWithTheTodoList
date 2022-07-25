namespace DownWithTheTodoList.Ms.Users.Context;

public class UsersContext : DbContext
{
    public DbSet<User> Users => Set<User>();
 
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(x => x.Id)
            .HasName("Id");

        modelBuilder.Entity<User>()
            .Property(x => x.NickName)
            .HasColumnName("NickName");
        
        modelBuilder.Entity<User>()
            .Property(x => x.Password)
            .HasColumnName("Pass");

    }
}