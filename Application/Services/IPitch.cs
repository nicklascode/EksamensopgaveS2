using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IPitch
    {
        Task<IReadOnlyList<PitchDto>> GetPitchesAsync();
        Task<PitchDto> GetPitch(int index);
    }
}
