using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows;
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
                //FIXME: add email format validation with REGEX
                if (value.Length > 50)
                {
                    throw new ArgumentException("Email length must be at most 50 characters long");
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
                } else
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
            return $"{FirstName} {LastName} with ID: {Id}";
        }

        public virtual List<Course> Courses { get; set; }

    }
}
