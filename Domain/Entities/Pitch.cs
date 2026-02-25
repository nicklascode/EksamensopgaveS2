using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Pitch
    {
        private int _id;
        private int _index;

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

        public int Index
        {
            get => _index;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Index must be a positive integer.");
                }
                _index = value;
            }
        }
    }
}
