using System;
using System.Data.Common;
using App.API.DataAccess.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace App.API.Tests.TestSQL;

#region SqliteInMemory
public class SqliteInMemoryItemsControllerTest : ItemsControllerTest, IDisposable
{
    private readonly DbConnection _connection;

    public SqliteInMemoryItemsControllerTest()
        : base(
            new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options)
    {
        _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
    }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");

        connection.Open();

        return connection;
    }

    void IDisposable.Dispose() => _connection.Dispose();
}
#endregion
