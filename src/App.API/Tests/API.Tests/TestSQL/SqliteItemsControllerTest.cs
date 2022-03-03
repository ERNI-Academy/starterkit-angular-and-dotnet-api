using App.API.DataAccess.Contexts;

using Microsoft.EntityFrameworkCore;

namespace App.API.Tests.TestSQL;

#region SqliteItemsControllerTest
public class SqliteItemsControllerTest : ItemsControllerTest
{
    public SqliteItemsControllerTest()
        : base(
            new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite("Filename=abb-fta.db")
                .Options)
    {
    }
}
#endregion
