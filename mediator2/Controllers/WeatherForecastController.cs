using mediator2.dom;
using mediator2.Querrys;
using Microsoft.AspNetCore.Mvc;

namespace mediator2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public Response Get([FromServices] Requist<QuerryDomeinClasses, Response> handler, [FromQuery] int id)
        {
            return handler.Handle(new QuerryDomeinClasses(id));
        }

        
    }
}
