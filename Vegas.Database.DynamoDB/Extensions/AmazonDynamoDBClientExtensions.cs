using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Extensions
{
    public static class AmazonDynamoDBClientExtensions
    {
        public static async Task<List<string>> GetTableNamesAsync(this IAmazonDynamoDB client)
        {
            var tables = new List<string>();
            var tablesResponse = await client.ListTablesAsync();
            if (tablesResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                tables.AddRange(tablesResponse.TableNames);
            }
            return tables;
        }

        public static async Task CreateTablesAsync(this IAmazonDynamoDB client, Assembly assembly)
        {
            var tableNames = await GetTableNamesAsync(client);
            var typeOfEntities = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(DynamoEntity)));
            foreach (var typeOfEntity in typeOfEntities)
            {
                if (tableNames.Contains(typeOfEntity.Name))
                {
                    continue;
                }
                await CreateTableAsync(client, typeOfEntity);
            }
        }

        public static async Task DeleteTablesAsync(this IAmazonDynamoDB client)
        {
            var tableNames = await GetTableNamesAsync(client);
            foreach (var tableName in tableNames)
            {
                await client.DeleteTableAsync(tableName);
            }
        }

        private static async Task CreateTableAsync(IAmazonDynamoDB client, Type typeOfEntity)
        {
            var request = new CreateTableRequest
            {
                TableName = typeOfEntity.Name,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("Id", ScalarAttributeType.S),
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("Id", KeyType.HASH),
                },
                ProvisionedThroughput = new ProvisionedThroughput(readCapacityUnits: 1, writeCapacityUnits: 1)
            };

            var secondaryIndexProperties = typeOfEntity.GetProperties()
                .Where(x => x.GetCustomAttribute<DynamoDBGlobalSecondaryIndexHashKeyAttribute>() is not null);
            if (secondaryIndexProperties.Any())
            {
                request.GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>();
            }
            foreach (var property in secondaryIndexProperties)
            {
                var scalarAttrType = new ScalarAttributeType(property.PropertyType.Name.ToUpper().First().ToString());
                request.AttributeDefinitions.Add(new AttributeDefinition(property.Name, scalarAttrType));
                request.GlobalSecondaryIndexes.Add(new GlobalSecondaryIndex
                {
                    IndexName = $"{property.Name}-index",
                    Projection = new Projection
                    {
                        ProjectionType = ProjectionType.ALL
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement(property.Name, KeyType.HASH)
                    },
                    ProvisionedThroughput = new ProvisionedThroughput(readCapacityUnits: 1, writeCapacityUnits: 1)
                });
            }
            await client.CreateTableAsync(request);
        }
    }
}
