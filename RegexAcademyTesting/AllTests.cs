using RegexAcademy;
using RegexAcademy.Models;

namespace RegexAcademyTesting
{

    [TestFixture]
    public class Tests
    {
        private User _user;
        private Teacher _teacher;
        private Student _student;
        private Course _course;

        [SetUp]
        public void Setup()
        {
            _user = new User();
            _teacher = new Teacher();
            _student = new Student();
            _course = new Course();
        }

        // USERS
        [Test]
        public void User_Password_RegexValidation_Works()
        {

            _user.Password = "password123";
            bool passwordIsValid;

            passwordIsValid = User.isPasswordValid(_user.Password, out string error);

            Assert.IsFalse(passwordIsValid);
        }

        [Test]
        public void Testing_HasSpecialCharacters()
        {
            _user.Username = "sdfafs---++*";

            bool yes = Globals.HasSpecialChars(_user.Username);
            Assert.IsTrue(yes);
        }


        [Test]
        public void IsUserNameValidTest()
        {
            _user.Username = "RandomUser22$";
            var result = User.isUsernameValid(_user.Username, out string error);
            Assert.IsTrue(result, "If failed, verify Regex in Models/User.cs");
        }


        [Test]
        public void IsUserNameInvalidTest()
        {
            _user.Username = "*Admin*";
            var result = User.isUsernameValid(_user.Username, out string error);
            Assert.IsFalse(result, "If failed, verify Regex in Models/User.cs");
        }

        //Students

        [Test]
        public void TestFirstNameArgumentExcep()
        {
            Assert.Throws<ArgumentException>(() => _student.FirstName = "Ba rry");
        }

        [Test]
        public void TestLastNameArgumentExcep()
        {
            Assert.Throws<ArgumentException>(() => _student.LastName = "Ter!rry");
        }

        [Test]
        public void TestDobArgumentExcep()
        {
            Assert.Throws<ArgumentException>(() => _student.DateOfBirth = DateTime.MaxValue);
            Assert.Throws<ArgumentException>(() => _student.DateOfBirth = DateTime.MinValue);
        }

        [Test]
        public void IsCourseNameValid_Test()
        {
            string test = "";
            //string test = "En";
            //string test = "English%";
            Assert.IsFalse(_course.CourseName == test, "If failed, verify Models/Course.cs");
        }


    }
}