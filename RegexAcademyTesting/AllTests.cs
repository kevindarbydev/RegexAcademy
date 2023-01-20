using RegexAcademy.Models;

namespace RegexAcademyTesting
{
    public class Tests
    {
        [Test]
        public void User_Password_RegexValidation_Works()
        {
            // ARRANGE 
            var user1 = new User();
            user1.Password = "password123";
            bool passwordIsValid;

            // ACT
            passwordIsValid = User.isPasswordValid(user1.Password, out string error);


            // ASSERT
            Assert.IsFalse(passwordIsValid);
        }
    }
}