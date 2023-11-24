using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configuration; 

public class YDbContext : DbContext{
    
    public YDbContext(DbContextOptions<YDbContext> options) : base(options) {
    }
    
    public DbSet<Yeet> Yeets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserFollowsUser> UserFriends { get; set; } = null!;
    public DbSet<UserLikesYeet> UserLikesYeets { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFollowsUser>()
            .HasKey(ufu => new {ufu.MasterId, ufu.SlaveId});
        
        modelBuilder.Entity<UserFollowsUser>()
            .HasOne(ufu => ufu.Master)
            .WithMany()
            .HasForeignKey(ufu => ufu.MasterId);
        
        modelBuilder.Entity<UserFollowsUser>()
            .HasOne(ufu => ufu.Slave)
            .WithMany()
            .HasForeignKey(ufu => ufu.SlaveId);
        
        modelBuilder.Entity<Yeet>()
            .HasOne(y => y.User)
            .WithMany()
            .HasForeignKey(y => y.UserId);
        
        modelBuilder.Entity<UserLikesYeet>()
            .HasKey(uly => new {uly.YeetId, uly.UserId});
        
        modelBuilder.Entity<UserLikesYeet>()
            .HasOne(uly => uly.User)
            .WithMany()
            .HasForeignKey(uly => uly.UserId);

        modelBuilder.Entity<UserLikesYeet>()
            .HasOne(uly => uly.Yeet)
            .WithMany()
            .HasForeignKey(uly => uly.YeetId);
    }
}