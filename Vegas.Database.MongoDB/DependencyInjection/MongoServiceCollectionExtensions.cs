using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Vegas.Database.MongoDB.Context;
using Vegas.Database.MongoDB.Repository;

namespace Vegas.Database.MongoDB.DependencyInjection
{
    public static class MongoServiceCollectionExtensions
    {
        public static void AddMongoAsyncRepository(this IServiceCollection services, string connectionString, string dbName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException(nameof(dbName));
            }

            ConventionRegistry.Register("EnumStringConvention", new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            }, type => true);

            // It is recommended to store a MongoClient instance in a global place,
            // either as a static variable or in an IoC container with a singleton lifetime.
            services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
            services.AddScoped(sp => new MongoDbContext(sp.GetRequiredService<IMongoClient>().GetDatabase(dbName)));
            services.AddScoped(typeof(IMongoAsyncRepository<>), typeof(MongoAsyncRepository<>));
        }
    }
}
