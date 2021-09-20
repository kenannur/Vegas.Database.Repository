using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Vegas.Database.DynamoDB.Repository;

namespace Vegas.Database.DynamoDB.DependencyInjection
{
    public static class DynamoServiceCollectionExtensions
    {
        public static void AddDynamoAsyncRepository(this IServiceCollection services, AWSCredentials credentials, RegionEndpoint region)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (credentials is null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }
            if (region is null)
            {
                throw new ArgumentNullException(nameof(region));
            }
            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>(sp => new AmazonDynamoDBClient(credentials, region));
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddScoped(typeof(IDynamoAsyncRepository<>), typeof(DynamoAsyncRepository<>));
        }
    }
}
