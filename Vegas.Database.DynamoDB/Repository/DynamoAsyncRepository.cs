using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Repository
{
    public class DynamoAsyncRepository<TEntity> : IDynamoAsyncRepository<TEntity>
        where TEntity : DynamoEntity
    {
        protected readonly IAmazonDynamoDB Client;
        protected readonly IDynamoDBContext Context;

        public DynamoAsyncRepository(IAmazonDynamoDB client, IDynamoDBContext context)
        {
            Client = client;
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await Context.SaveAsync(entity, ct);
            return entity;
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity, ct);
            }
        }

        public async Task DeleteAsync(string id, CancellationToken ct = default)
        {
            await Context.DeleteAsync<TEntity>(id, ct);
        }

        public async Task DeleteManyAsync(IEnumerable<string> ids, CancellationToken ct = default)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id, ct);
            }
        }

        public async Task<TEntity> GetAsync(string id, CancellationToken ct = default)
        {
            return await Context.LoadAsync<TEntity>(id, ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            await Context.SaveAsync(entity, ct);
            return entity;
        }

        public async Task<List<string>> GetTablesAsync()
        {
            var tables = new List<string>();
            var tablesResponse = await Client.ListTablesAsync();
            if (tablesResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                tables.AddRange(tablesResponse.TableNames);
            }
            return tables;
        }

        public async Task DeleteTablesAsync()
        {
            var tablesResponse = await Client.ListTablesAsync();
            if (tablesResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                foreach (var tableName in tablesResponse.TableNames)
                {
                    await Client.DeleteTableAsync(tableName);
                }
            }
        }

        public async Task CreateTablesAsync()
        {
            var assemblyOfEntities = Assembly.GetCallingAssembly();
            var typeOfEntities = assemblyOfEntities.GetTypes().Where(type => type.IsSubclassOf(typeof(DynamoEntity)));

            var tablesResponse = await Client.ListTablesAsync();
            if (tablesResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                foreach (var typeOfEntity in typeOfEntities)
                {
                    if (tablesResponse.TableNames.Contains(typeOfEntity.Name))
                    {
                        continue;
                    }
                    await CreateTableAsync(typeOfEntity);
                }
            }
        }

        private async Task CreateTableAsync(Type typeOfEntity)
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
            await Client.CreateTableAsync(request);
        }
    }
}
