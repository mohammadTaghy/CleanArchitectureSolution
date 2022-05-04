using Application.Decorators;
using Application.Dtos;
using Application.UseCases.Weather;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        private readonly Messages _messages;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Messages messages)
        {
            _logger = logger;
            _messages = messages;
        }

        
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var weatherList = _messages.Dispatch<List<WeatherDto>>(new GetWeatherListQuery(Summaries));
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = weatherList[Random.Shared.Next(weatherList.Count)].Name
            })
            .ToArray();
        }

        [HttpPost(Name = "RenameWeather")]
        public JsonResult RenameWeather(CancellationTokenSource cancellationToken)
        {
            try
            {
                cancellationToken.CancelAfter(1000);
                cancellationToken.Token.ThrowIfCancellationRequested();
                return new JsonResult(_messages.Dispatch(new RenameWeatherCommand(5), cancellationToken.Token));
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("\nTasks cancelled: timed out.\n");
                return new JsonResult( "\nTasks cancelled: timed out.\n");
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cancellationToken.Dispose();
            }
          
        }
    }
}