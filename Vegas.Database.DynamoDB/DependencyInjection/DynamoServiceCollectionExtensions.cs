using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Vegas.Database.DynamoDB.Repository;
using Vegas.Database.DynamoDB.Setting;

namespace Vegas.Database.DynamoDB.DependencyInjection
{
    public static class DynamoServiceCollectionExtensions
    {
        public static void AddDynamoAsyncRepository(this IServiceCollection services, IDynamoDBSettings settings)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var region = RegionEndpoint.GetBySystemName(settings.Region);

            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>(sp =>
            {
                return new AmazonDynamoDBClient(new BasicAWSCredentials(settings.AccessKey, settings.SecretKey), RegionEndpoint.EUNorth1);
            });
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddScoped(typeof(IDynamoAsyncRepository<>), typeof(DynamoAsyncRepository<>));
        }
    }
}
