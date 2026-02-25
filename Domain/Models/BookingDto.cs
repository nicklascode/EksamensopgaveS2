using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public record BookingDto(int UserId, int PitchId, DateTime StartTime, DateTime EndTime);
}

