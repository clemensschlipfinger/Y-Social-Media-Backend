using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configuration; 

public class YDbContext : DbContext{
    
    public YDbContext(DbContextOptions<YDbContext> options) : base(options) {
    }
    
    public DbSet<Yeet> Yeets { get; set; } = null!;
    public DbSet<Yomment> Yomments { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserFollowsUser> UserFriends { get; set; } = null!;
    public DbSet<YeetHasTags> YeetHasTags { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFollowsUser>()
            .HasKey(ufu => new {ufu.FollowerId, ufu.FollowingId});
        
        modelBuilder.Entity<UserFollowsUser>()
            .HasOne(ufu => ufu.Following)
            .WithMany()
            .HasForeignKey(ufu => ufu.FollowingId);
        
        modelBuilder.Entity<UserFollowsUser>()
            .HasOne(ufu => ufu.Follower)
            .WithMany()
            .HasForeignKey(ufu => ufu.FollowerId);
        
        modelBuilder.Entity<Yeet>()
            .HasOne(y => y.User)
            .WithMany()
            .HasForeignKey(y => y.UserId);
        
        modelBuilder.Entity<Yomment>()
            .HasOne(y => y.User)
            .WithMany()
            .HasForeignKey(y => y.UserId);
        
        modelBuilder.Entity<Yomment>()
            .HasOne(y => y.Yeet)
            .WithMany(y => y.Yomments)
            .HasForeignKey(y => y.YeetId);
        
        modelBuilder.Entity<YeetHasTags>()
            .HasKey(ufu => new {ufu.YeetId, ufu.TagId});
        
        modelBuilder.Entity<YeetHasTags>()
            .HasOne(y => y.Tag)
            .WithMany()
            .HasForeignKey(y => y.TagId);
        
        modelBuilder.Entity<YeetHasTags>()
            .HasOne(y => y.Yeet)
            .WithMany(y => y.Tags)
            .HasForeignKey(y => y.YeetId);
    }
}