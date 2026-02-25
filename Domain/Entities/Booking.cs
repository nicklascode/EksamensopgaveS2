using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Booking
    {
        private int _id;
        private int _userId;
        private int _pitchId;
        private DateTime _startTime;
        private DateTime _endTime;

        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Id must be a positive integer.");
                }
                _id = value;
            }
        }
        public int UserId
        {
            get => _userId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("UserId must be a positive integer.");
                }
                _userId = value;
            }
        }
        public int PitchId
        {
            get => _pitchId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("PitchId must be a positive integer.");
                }
                _pitchId = value;
            }
        }
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value == default)
                {
                    throw new ArgumentException("StartTime must be a valid date and time.");
                }
                _startTime = value;
            }
        }
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (value == default)
                {
                    throw new ArgumentException("EndTime must be a valid date and time.");
                }
                if (value <= StartTime)
                {
                    throw new ArgumentException("EndTime must be after StartTime.");
                }
                _endTime = value;
            }
        }

        // Navigation properties
        public User User { get; set; } 
        public Pitch Pitch { get; set; }
    }
}
