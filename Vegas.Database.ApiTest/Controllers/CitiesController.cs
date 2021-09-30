using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vegas.Database.ApiTest.Entities;
using Vegas.Database.ApiTest.Repositories;

namespace Vegas.Database.ApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesAsync([FromQuery] string baseId)
        {
            var cities = await _cityRepository.GetCitiesByBaseIdAsync(baseId);
            return Ok(cities);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetCityAsync([FromQuery] string baseId, [FromQuery] string name)
        {
            var city = await _cityRepository.GetCityByByBaseIdAndNameAsync(baseId, name);
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCitiesAsync()
        {
            var list = new List<City>
            {
                new City
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    Name = "İstanbul",
                    BaseId = "123"
                },
                new City
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    Name = "İzmir",
                    BaseId = "123"
                },
                new City
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    Name = "Kavala",
                    BaseId = "456"
                }
            };
            await _cityRepository.AddManyAsync(list);
            return Ok();
        }

    }
}
