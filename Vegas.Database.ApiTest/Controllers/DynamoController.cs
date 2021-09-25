using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc;
using Vegas.Database.DynamoDB.Extensions;

namespace Vegas.Database.ApiTest.Controllers
{
    [Route("[controller]/Tables")]
    public class DynamoController : Controller
    {
        private readonly IAmazonDynamoDB _client;
        public DynamoController(IAmazonDynamoDB client) => _client = client;

        [HttpGet]
        public async Task<IActionResult> GetTablesAsync()
        {
            var result = await _client.GetTableNamesAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTablesAsync()
        {
            await _client.CreateTablesAsync(Assembly.GetExecutingAssembly());
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTablesAsync()
        {
            await _client.DeleteTablesAsync();
            return Ok();
        }

    }
}
