using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vegas.Database.Relational.Repository;

namespace Vegas.Database.Relational.DependencyInjection
{
    public static class RelationalDatabasesServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreAsyncRepository<TDbContext>(this IServiceCollection services, string connectionString)
            where TDbContext : DbContext
        {
            ThrowIfNull(services, connectionString);
            services.AddDbContext<TDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            return services;
        }

        public static IServiceCollection AddSqlServerAsyncRepository<TDbContext>(this IServiceCollection services, string connectionString)
            where TDbContext : DbContext
        {
            ThrowIfNull(services, connectionString);
            services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            return services;
        }

        private static void ThrowIfNull(IServiceCollection services, string connectionString)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
        }
    }
}
