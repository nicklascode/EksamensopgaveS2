using DataAccess.Repositories;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BookingService(IGenericRepository<Booking> bookingRepo, IGenericRepository<Pitch> pitchRepo) : IBooking
    {
        public async Task<BookingDto> BookPitchAsync(BookingDto bookingDto)
        {
            if (!await IsValidBooking(bookingDto))
                throw new InvalidOperationException("The booking overlaps with an existing booking or has invalid time range.");

            var booking = new Booking
                {
                UserId = bookingDto.UserId,
                PitchId = bookingDto.PitchId,
                StartTime = bookingDto.StartTime,
                EndTime = bookingDto.EndTime
            };

            await bookingRepo.AddAsync(booking);
            return bookingDto;
        }

        public async Task<BookingDto> EditBookingAsync(int index, BookingDto bookingDto)
        {
                if (!await IsValidBooking(bookingDto))
                    throw new InvalidOperationException("The booking overlaps with an existing booking or has invalid time range.");
    
                var existingBookings = await bookingRepo.GetAllAsync();
                if (index < 0 || index >= existingBookings.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Invalid booking index.");
    
                var bookingToEdit = existingBookings[index];
                bookingToEdit.UserId = bookingDto.UserId;
                bookingToEdit.PitchId = bookingDto.PitchId;
                bookingToEdit.StartTime = bookingDto.StartTime;
                bookingToEdit.EndTime = bookingDto.EndTime;
    
                await bookingRepo.UpdateAsync(bookingToEdit);
    
                return bookingDto;
        }

        public async Task<IReadOnlyList<BookingDto>> GetBookingsAsync()
        {
            var bookings = await bookingRepo.GetAllAsync();

            IReadOnlyList<BookingDto> bookingDtos = bookings.Select(b => new BookingDto
            (
                b.UserId,
                b.PitchId,
                b.StartTime,
                b.EndTime
            )).ToList();

            return bookingDtos;
        }

        public async Task<IReadOnlyList<BookingDto>> GetBookingsInBetweenPeriodAsync(DateTime start, DateTime end)
        {
            var bookings = await bookingRepo.GetAllAsync();
            IReadOnlyList<BookingDto> bookingDtos = bookings.Where(b => b.StartTime >= start && b.EndTime <= end)
                .Select(b => new BookingDto
                (
                    b.UserId,
                    b.PitchId,
                    b.StartTime,
                    b.EndTime
                )).ToList();
            return bookingDtos;
        }

        private async Task<bool> IsValidBooking(BookingDto bookingDto)
        {
            if (bookingDto is null)
                throw new ArgumentNullException(nameof(bookingDto));

            // Ensure pitch exists
            var pitches = await pitchRepo.GetAllAsync();
            var pitchExists = pitches.Any(p => p.Id == bookingDto.PitchId);
            if (!pitchExists)
                throw new InvalidOperationException($"Pitch with id {bookingDto.PitchId} does not exist.");

            // Invalid time range
            if (bookingDto.EndTime <= bookingDto.StartTime)
                return false;

            var existingBookings = await bookingRepo.GetAllAsync();

            var hasOverlap = existingBookings.Any(b =>
                b.PitchId == bookingDto.PitchId &&
                bookingDto.StartTime < b.EndTime &&
                bookingDto.EndTime > b.StartTime
            );

            return !hasOverlap;
        }
    }
}
