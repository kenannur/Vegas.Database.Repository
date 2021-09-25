using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vegas.Database.ApiTest.Entities;
using Vegas.Database.DynamoDB.Repository;

namespace Vegas.Database.ApiTest.Controllers
{
    [Route("[controller]")]
    public class DynamoController : Controller
    {
        private readonly IDynamoAsyncRepository<User> _repository;

        public DynamoController(IDynamoAsyncRepository<User> repository)
        {
            _repository = repository;
        }

        [HttpGet("Tables")]
        public async Task<IActionResult> GetTablesAsync()
        {
            var result = await _repository.GetTablesAsync();
            return Ok(result);
        }

        [HttpPost("Tables")]
        public async Task<IActionResult> CreateTablesAsync()
        {
            await _repository.CreateTablesAsync();
            return Ok();
        }

        [HttpDelete("Tables")]
        public async Task<IActionResult> DeleteTablesAsync()
        {
            await _repository.DeleteTablesAsync();
            return Ok();
        }

    }
}
