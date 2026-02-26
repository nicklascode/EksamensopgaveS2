using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Application.Services
{
    public class WeatherService(IHttpClientFactory factory, IOptions<WeatherApiOptions> options) : IWeather
    {

        public async Task<WeatherDto> GetWeatherAsync(double lat, double lon)
        {
            var client = factory.CreateClient();

            var url = $"{options.Value.BaseUrl}onecall?lat={lat}.44&lon={lon}.04&exclude=hourly,daily&appid={options.Value.ApiKey}";
            var response = await client.GetAsync(url);

            Console.WriteLine(url);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var root = doc.RootElement;
            var current = root.GetProperty("current");

            var temp = current.GetProperty("temp").GetDouble();
            var feelsLike = current.GetProperty("feels_like").GetDouble();

            var weatherArr = current.GetProperty("weather");
            var firstWeather = weatherArr[0];
            var status = firstWeather.GetProperty("main").GetString() ?? string.Empty;
            var description = firstWeather.GetProperty("description").GetString() ?? string.Empty;

            WeatherDto weatherDto = new WeatherDto
            (
                Temperature: temp,
                FeelsLike: feelsLike,
                Status: status,
                Description: description
            );

            return weatherDto;
        }
    }
}
