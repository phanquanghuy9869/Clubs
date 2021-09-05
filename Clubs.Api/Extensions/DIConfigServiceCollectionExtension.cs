using System;
using System.Data;
using Clubs.Api.CQRS.Queries;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Clubs.Infrastructure.Context;
using Clubs.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clubs.Api.Extensions
{
    public static class DIConfigServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            // Db context
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Clubs.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            services.AddDbContext<ClubDbContext>(options => options.UseSqlite(connection));
            services.AddScoped<IDbConnection>((sp) => connection);
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IClubQueries, ClubQueries>();
            return services;
        }
    }
}
