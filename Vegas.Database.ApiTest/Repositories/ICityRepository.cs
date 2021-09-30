using System.Collections.Generic;
using System.Threading.Tasks;
using Vegas.Database.ApiTest.Entities;
using Vegas.Database.DynamoDB.Repository;

namespace Vegas.Database.ApiTest.Repositories
{
    public interface ICityRepository : IDynamoAsyncRepository<City>
    {
        Task<List<City>> GetCitiesByBaseIdAsync(string baseId);
    }
}
