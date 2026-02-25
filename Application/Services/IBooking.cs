using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IBooking
    {
        Task<IReadOnlyList<BookingDto>> GetBookingsInBetweenPeriodAsync(DateTime start, DateTime end);
        Task<IReadOnlyList<BookingDto>> GetBookingsAsync();
        Task<BookingDto> BookPitchAsync(BookingDto bookingDto);
        Task<BookingDto> EditBookingAsync(int index, BookingDto bookingDto); // As they could hold multiple bookings, we can't just use the dto as the identifier, we need an index to identify which booking they want to edit
    }
}
