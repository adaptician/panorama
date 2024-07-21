using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Panorama.EntityFrameworkCore
{
    public static class PanoramaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PanoramaDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PanoramaDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
