using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RegexAcademy
{
    public class Globals
    {
        public static RegexAcademyDbContext dbContext;

        public static bool HasSpecialChars(string toBeValidated)
        {
            return toBeValidated.Any(ch => !char.IsLetterOrDigit(ch)); //iterates through a string and returns true if any char is a special char. placing this in Globals as I expect this will be useful in several places
        }

        public static CroppedBitmap CropsImage(BitmapImage profileImage)
        {
            if (profileImage.PixelHeight > 400 || profileImage.PixelWidth > 400)
            {
                try
                {
                    var croppedProfileImage = new CroppedBitmap(profileImage, new Int32Rect(125, 125, 250, 250));
                    return croppedProfileImage;
                }
                catch (SystemException)
                {

                }

            }
            return null;

        }

        public static bool isLoggedIn = false;
    }
}
