using System;
using System.ComponentModel.DataAnnotations;

namespace RegexAcademy.Models
{
    public class Student
    {
        public Student(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
        public Student()
        {

        }

        [Key]
        public int Id { get; set; }

        private string _firstName;

        [Required]
        [StringLength(40)]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value.Length < 1 || value.Length > 40) // needs more validation, ie. check for special characters
                {
                    throw new ArgumentException("First name length should be 1-40 characters long");
                }
                _firstName = value;
            }
        }

        private string _lastName;

        [Required]
        [StringLength(40)]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value.Length < 1 || value.Length > 40) // needs more validation, ie. check for special characters
                {
                    throw new ArgumentException("Last name length should be 1-40 characters long");
                }
                _lastName = value;
            }
        }

        private DateTime _dateOfBirth;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value.Year > 2023)//placeholder, will change
                {
                    throw new ArgumentException("You've entered an invalid date. Try again");
                }
                _dateOfBirth = value;
            }
        }

        public override string ToString()
        {
            return $"Student ID:{Id}, First name: {_firstName}, Last name: {_lastName}, Date of birth: {_dateOfBirth}";
        }

        //public virtual List<StudentCourse> StudenCourseList { get; set; }
    }
}
