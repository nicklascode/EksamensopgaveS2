using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PitchController(IPitch pitchService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetPitches()
        {
            var pitches = await pitchService.GetPitchesAsync();
            if(pitches == null || pitches.Count == 0)
                return NotFound("No pitches found!");

            return Ok(pitches);
        }

        [HttpGet("{index}")]
        public async Task<ActionResult> GetPitch(int index)
        {
            var pitch = await pitchService.GetPitch(index);
            if(pitch == null)
                return NotFound("Pitch not found!");
            return Ok(pitch);
        }
    }
}
