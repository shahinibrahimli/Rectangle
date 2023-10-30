using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rectangle.Domain.Contracts;

namespace Rectangle.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPooledDbContextFactory<RectangleDbContext>(
                (_,options) =>
                {
                    var connectionString = configuration.GetConnectionString("RectangleConnectionString");
                    Console.WriteLine("DBConnectionStr: " + SafeConnectionString(connectionString));
                    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(1800));
                });
            services.AddScoped<IUnitOfWorkInstanceBuilder, UnitOfWorkInstanceBuilder>();
            services.AddScoped<IAuditContextFactory, AuditContextFactory>();
            return services;
        }
        private static string SafeConnectionString(string? connectionString)
        {
            if (connectionString == null)
                return "<null>";
            if (string.IsNullOrWhiteSpace(connectionString))
                return "<empty>";

            try
            {
                var parts = connectionString.Split(";", StringSplitOptions.RemoveEmptyEntries);
                var reconstructedConnectionString = string.Join(";", parts.Where(itm => !itm.ToLowerInvariant().StartsWith("password")));
                return reconstructedConnectionString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
