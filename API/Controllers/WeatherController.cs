using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController(IWeather weather) : ControllerBase
    {
        [HttpGet("getWeather")]
        public async Task<ActionResult<WeatherDto>> GetWeather([FromQuery]WeatherLocationDto locationDto)
        {
            if (locationDto == null)
                return BadRequest("Not a valid location");

            var weatherData = await weather.GetWeatherAsync(locationDto.lat, locationDto.lon);
            if (weatherData == null)
                return StatusCode(500, "Failed to fetch weather");

            return Ok(weatherData);
        }
    }
}
