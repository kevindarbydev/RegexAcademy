using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexAcademy
{
    public class Users
    {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        protected virtual string PasswordStored { get; set; }

        [NotMapped]
        public string Password
        {
            get 
            { 
                return PasswordStored; 
            } 
            set
            {
                PasswordStored = PasswordEncryptor.EncryptPassword(value);
            }
        }

    }   
}
