using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace tts.EntityFrameworkCore
{
    public static class ttsDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ttsDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ttsDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
