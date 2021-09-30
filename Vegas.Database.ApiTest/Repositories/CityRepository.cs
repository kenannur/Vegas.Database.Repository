using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Vegas.Database.ApiTest.Entities;
using Vegas.Database.DynamoDB.Repository;

namespace Vegas.Database.ApiTest.Repositories
{
    public class CityRepository : DynamoAsyncRepository<City>, ICityRepository
    {
        public CityRepository(IAmazonDynamoDB client, IDynamoDBContext context)
            : base(client, context)
        { }

        public async Task<List<City>> GetCitiesByBaseIdAsync(string baseId)
        {
            var result = await Context.QueryAsync<City>(baseId)
                                      .GetRemainingAsync();
            return result;
        }

        public async Task<City> GetCityByByBaseIdAndNameAsync(string baseId, string name)
        {
            return await Context.LoadAsync<City>(baseId, name);
        }
    }
}
