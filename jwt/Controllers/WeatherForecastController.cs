using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("[controller]")]

    [AllowAnonymous]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServece2 _servece2;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IServece2 servece2)
        {
            _logger = logger;
            _servece2 = servece2;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public Task Get()
        {
            _servece2.PrinteService2("wesam I love you");
            return Task.CompletedTask;
        }
    }
}