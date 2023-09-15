using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Server.Data.DbContexts;
using Server.Entities;

namespace Testing.Server.Database;

public class TestDatabaseFixture
{

    private static readonly object _lock = new();
    private static bool _databaseInitialized;
    private static readonly string _connectionString = 
        Environment.GetEnvironmentVariable(new ConfigurationBuilder()
            .SetBasePath(Path.Combine(AppContext.BaseDirectory))
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("TestingConnection")!
            .Replace("%", ""), EnvironmentVariableTarget.User)!;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (_databaseInitialized)
            {
                return;
            }

            var collection = new ServiceCollection();

            collection.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(_connectionString));

            collection.AddIdentityCore<User>(o =>
            {
                o.User.RequireUniqueEmail = true;
                o.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";

                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 1;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            collection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
            

            var provider = collection.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();




                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    context.Database.EnsureDeleted();
                    var created = context.Database.EnsureCreated();
                    Assert.True(created);

                   var result = manager.CreateAsync(new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "test",
                        PhoneNumber = "123",
                        Email = "h@gmail.com",
                        Followers = null,
                        Followings = null,
                        RecievedFollowRequests = null,
                        SentFollowRequests = null,
                        Nickname = "test",
                        State = AccountState.Active,
                    }, "password")
                        .GetAwaiter().GetResult();

                    int x = context.SaveChanges();
                }
            }

            _databaseInitialized = true;
        }
    }

    public AppDbContext CreateContext()
        => new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_connectionString)
                .Options);
}