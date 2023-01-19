using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegexAcademy.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        protected virtual string PasswordStored { get; set; }


        [NotMapped]
        public string Password
        {
            get { return PasswordStored; }
            set
            {
                PasswordStored = PasswordEncryptor.EncryptPassword(value);
            }
        }

    }
}
