using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegexAcademy.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public Teacher() { }

        public Teacher(string firstName, string lastName, string email, byte[] profileImage, bool availability)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ProfileImage = profileImage;
            Availability = availability;
        }


        private string _firstName;

        [Required]
        [StringLength(50)]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new ArgumentException("First name length must be 2-50 characters long");
                }
                _firstName = value;
            }
        }

        private string _lastName;

        [Required]
        [StringLength(50)]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new ArgumentException("Last name length must be 2-50 characters long");
                }
                _lastName = value;
            }
        }

        private string _email;

        [StringLength(50)]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new ArgumentException("Email length must be 2-50 characters long");
                }
                _email = value;
            }
        }

        public byte[] ProfileImage { get; set; }

        private bool _availability;

        public bool Availability
        {
            get
            {
                return _availability;
            }
            set
            {
                if (value == false)
                {
                    throw new ArgumentException("Sorry, this teacher is unavailable");
                }
                _availability = value;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public virtual List<Course> Courses { get; set; }

    }
}
