using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vegas.Database.ApiTest.Entities;
using Vegas.Database.DynamoDB.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            await _repository.CreateTablesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTablesAsync()
        {
            await _repository.DeleteTablesAsync();
            return Ok();
        }

    }
}
