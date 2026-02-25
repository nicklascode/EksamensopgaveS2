using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public record WeatherDto(double Temperature, double FeelsLike, string Status, string Description);
}
