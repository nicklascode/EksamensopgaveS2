using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class WeatherApiOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
