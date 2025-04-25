using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Runtime.InteropServices;
using Server.Data.DbContexts;

namespace Testing.Server.Database;

public class SchemaTests : IClassFixture<TestDatabaseFixture>, IDisposable
{
    private readonly TestDatabaseFixture _fixture;

    public SchemaTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Database_ShouldCreateDbContext()
    {
        //arrange
        var context = _fixture.CreateContext();
        //act
        
        //assert
        Assert.True(true);
    }

    public void Dispose()
    {
        _fixture.CreateContext().Database.EnsureDeleted();
    }
}
