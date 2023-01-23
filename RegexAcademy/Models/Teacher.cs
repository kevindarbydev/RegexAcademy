using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

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
                if (Globals.HasSpecialChars(value))
                {
                    throw new ArgumentException("First name cannot contain special characters.");
                }
                if (!Regex.IsMatch(value, @"^[^0-9]+$"))
                {
                    throw new ArgumentException("First name cannot contain numbers");
                }
                string CapitalizeName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower());
                _firstName = CapitalizeName;
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
                if (Globals.HasSpecialChars(value))
                {
                    throw new ArgumentException("Last name cannot contain special characters.");
                }
                if (!Regex.IsMatch(value, @"^[^0-9]+$"))
                {
                    throw new ArgumentException("Last name cannot contain numbers");
                }
                string CapitalizeName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower()); //capitalize first letter, could be redundant
                _lastName = CapitalizeName;
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

                if (value.Length > 50 || value.Length < 6)
                {
                    throw new ArgumentException("Email length must 6-50 characters long.");
                }
                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*$"))
                {
                    throw new ArgumentException("Must be email format.");
                }
                _email = value;
            }
        }

        public byte[] ProfileImage { get; set; }

        [NotMapped]
        public BitmapSource ProfileImageToShow
        {
            get
            {
                if (ProfileImage == null || ProfileImage.Length == 0) return null;
                var image = new BitmapImage();
                using (var mem = new MemoryStream(ProfileImage))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();

                var croppedProfileImage = Globals.CropsImage(image);
                if (croppedProfileImage != null)
                {
                    return croppedProfileImage;
                }
                else
                {
                    return image;
                }

            }
        }

        public bool Availability { get; set; }

        [NotMapped]
        public string AvailabilityToShow
        {
            get
            {
                if (Availability == true)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }

        //helper methods for searching
        [NotMapped]
        public char DisplayFirstLetter => this.GetType().Name[0];

        public string NameConcatenation()
        {
            return FirstName + " " + LastName;
        }
        public override string ToString()
        {
            return $"Teacher ID: {Id};First Name: {FirstName};Last Name: {LastName};Email: {Email};Availability: {Availability}";
        }

        public virtual List<Course> Courses { get; set; }
    }
}
