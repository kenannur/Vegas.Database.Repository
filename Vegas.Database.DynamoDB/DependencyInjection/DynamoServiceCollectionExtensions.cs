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

            var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
            var region = RegionEndpoint.GetBySystemName(settings.Region);
            var client = new AmazonDynamoDBClient(credentials, region);

            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddScoped(typeof(IDynamoAsyncRepository<>), typeof(DynamoAsyncRepository<>));
        }
    }
}
