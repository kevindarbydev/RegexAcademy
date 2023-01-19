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
                if (value.Length < 1 || value.Length > 40)
                {
                    throw new ArgumentException("First name length should be 1-40 characters long");
                }
                if (Globals.HasSpecialChars(value))
                {
                    throw new ArgumentException("First name cannot contain special chars.");
                }
                string CapitalizeName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower());
                _firstName = CapitalizeName;
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
                if (value.Length < 1 || value.Length > 40)
                {
                    throw new ArgumentException("Last name length should be 1-40 characters long");
                }
                if (Globals.HasSpecialChars(value))
                {
                    throw new ArgumentException("Last name cannot contain special chars.");
                }
                string CapitalizeName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower()); //capitalize first letter, could be redundant
                _lastName = CapitalizeName;
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
                if (value == null)//this should never occur
                {
                    throw new ArgumentException("Please make sure to select a date.");
                }
                if (value.Year < 1900 || value.Year > 2023)
                {
                    throw new ArgumentException("Please enter a valid date of birth.");
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
