using DataAccess.Repositories;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class PitchService(IGenericRepository<Pitch> pitchRepo) : IPitch
    {
        public async Task<PitchDto> GetPitch(int index)
        {
            var pitches = await pitchRepo.GetAllAsync();
            var pitch = pitches.FirstOrDefault(u => u.Index == index);

            if (pitch == null)
                return null;

            PitchDto pitchDto = new PitchDto(pitch.Id, pitch.Index);
            return pitchDto;
        }

        public async Task<IReadOnlyList<PitchDto>> GetPitchesAsync()
        {
            var pitches = await pitchRepo.GetAllAsync();
            var pitchDtos = pitches.Select(p => new PitchDto(p.Id, p.Index)).ToList();

            return pitchDtos;
        }
    }
}
