using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IBooking bookingService) : ControllerBase
    {
        [HttpGet("bookings")]
        public async Task<ActionResult> GetBookings()
        {
            var bookings = await bookingService.GetBookingsAsync();
            return Ok(bookings);
        }

        [HttpPost("book")]
        public async Task<ActionResult> BookPitch(BookingDto bookingDto)
        {
            try
            {
                var booking = await bookingService.BookPitchAsync(bookingDto);
                return Ok(booking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit/{index}")]
        public async Task<ActionResult> EditBooking(int index, BookingDto bookingDto)
        {
            try
            {
                var booking = await bookingService.EditBookingAsync(index, bookingDto);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("bookings/period")]
        public async Task<ActionResult> GetBookingsInBetweenPeriod(DateTime start, DateTime end)
        {
            var bookings = await bookingService.GetBookingsInBetweenPeriodAsync(start, end);
            return Ok(bookings);
        }

    }
}
