using System.Linq;

namespace RegexAcademy
{
    internal class Globals
    {
        public static RegexAcademyDbContext dbContext;

        public static bool HasSpecialChars(string toBeValidated)
        {
            return toBeValidated.Any(ch => !char.IsLetterOrDigit(ch)); //iterates through a string and returns true if any char is a special char. placing this in Globals as I expect this will be useful in several places
        }

        public static bool isLoggedIn = false;
    }
}
