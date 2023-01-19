using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RegexAcademy.Models
{
    public class User
    { 
        [Key]
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }


        public static bool isPasswordValid(string password, out string error)
        {
            if(!Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,20}$")) {

                error = "Password must be 8 to 20 characters long with at least 1 capital letter, 1 lower case letter, 1 digit and 1 special character.";
                return false;
            }

            error = null;
            return true;
        }
        public static bool isUsernameValid(string username, out string error)
        {
            if (!Regex.IsMatch(username, @"[A-Z]{1,1}[a-zA-Z0-9!/,;\-+\().*\@#$%\^&]{4,19}"))
            {
                error = "Username must be 5-20 characters and must begin with a capital letter followed by lowercase letters, digits and/or special characters";
                return false;
            }

            error = null;
            return true;
        }
    }
}
