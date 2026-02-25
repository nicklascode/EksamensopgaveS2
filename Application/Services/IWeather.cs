using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IWeather
    {
        Task<WeatherDto> GetWeatherAsync(double lat, double lon);
    }
}
