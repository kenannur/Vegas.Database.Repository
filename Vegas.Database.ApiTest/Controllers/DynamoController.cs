using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vegas.Database.ApiTest.Controllers
{
    [Route("[controller]")]
    public class DynamoController : Controller
    {
        public DynamoController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            
        }

    }
}
