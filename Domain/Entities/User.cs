using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        private int _id;
        private string _name = string.Empty;
        private string _email = string.Empty;

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

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Email cannot be null or empty.");
                }
                try
                {
                    var addr = new System.Net.Mail.MailAddress(value);
                    if (addr.Address != value)
                    {
                        throw new ArgumentException("Invalid email format.");
                    }
                }
                catch
                {
                    throw new ArgumentException("Invalid email format.");
                }
                _email = value;
            }
        }
    }
}
