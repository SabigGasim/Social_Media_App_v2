using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Entities;
using System.Linq.Expressions;

namespace Server.Data.DbContexts;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Timeline> Timelines { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<DirectChat> DirectChats { get; set; }
    public DbSet<DirectMessage> DirectMessages { get; set; }
    public DbSet<GroupChat> GroupChats { get; set; }
    public DbSet<GroupMessage> GroupMessages { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<FollowRequest> FollowRequests { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Follow>()
            .HasOne(follow => follow.Follower)
            .WithMany(User => User.Followings)
            .HasForeignKey(o => o.FollowerId)
            .OnDelete(DeleteBehavior.ClientSetNull);


        modelBuilder.Entity<Follow>()
            .HasOne(f => f.Following)
            .WithMany(f => f.Followers)
            .HasForeignKey(o => o.FollowingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<FollowRequest>()
            .HasOne(f => f.Follower)
            .WithMany(user => user.SentFollowRequests)
            .HasForeignKey(o => o.FollowerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<FollowRequest>()
            .HasOne(f => f.Following)
            .WithMany(user => user.RecievedFollowRequests)
            .HasForeignKey(o => o.FollowingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<GroupMessage>()
            .HasMany(gm => gm.ReadFrom)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
            "GroupMessageReadFrom",
            r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict),
            l => l.HasOne<GroupMessage>().WithMany().HasForeignKey("MessageId").OnDelete(DeleteBehavior.Restrict));


        ConfigureLikesTable(modelBuilder, user => user.LikedPosts);
        ConfigureLikesTable(modelBuilder, user => user.LikedComments);
        ConfigureLikesTable(modelBuilder, user => user.LikedReplies);
    }

    private void ConfigureLikesTable<TLikable>(
        ModelBuilder modelBuilder,
        Expression<Func<User, IEnumerable<TLikable>?>> inverseProperty) 
        where TLikable : class, ILikable
    {
        var genericTableName = typeof(TLikable).Name;

        modelBuilder.Entity<TLikable>()
            .HasMany(d => d.LikedFrom)
            .WithMany(inverseProperty)
            .UsingEntity<Dictionary<string, object>>($"{genericTableName}Likes",
            j => j.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull),
            l => l.HasOne<TLikable>()
                .WithMany()
                .HasForeignKey($"{genericTableName}Id")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull));
    }
}