using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LightningPT.EntityFrameworkCore.EntityFrameworkCore
{
    public static class LightningPTDbContextConfigurer<TDbContext> where TDbContext : DbContext
    {
        public static void Configure(DbContextOptionsBuilder<TDbContext> builder, string connectString)
        {
            builder.UseMySql(connectString);
        }

        public static void Configure(DbContextOptionsBuilder<TDbContext> builder, DbConnection dbConnection)
        {
            builder.UseMySql(dbConnection);
        }
    }
}