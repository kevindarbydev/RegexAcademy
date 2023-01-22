using RegexAcademy;
using RegexAcademy.Models;

namespace RegexAcademyTesting
{


    // ----------------------------------------------------
    // tests are difficult in WPF: you can't write them for methods in the code behind, only models.
    // ...so what that means is we can only test the few methods we have.
    // I have seen that it's possible to test if button clicks work and other WPF elements, so I'll look into that.
    // I have also read that seprating into a true MVVM pattern aids in testing somehow, but I'm not getting why/how it's any different than our setup.
    // How we were taught was to put the logic in xaml.cs code-behind, not to throw it all in the class/model/object files. 
    // Unfortunately, we may have to migrate validation + other methods into those files in order to meet the minimum 10 unit test threshold.
    //------------------------------------------------------
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

            // ACT
            passwordIsValid = User.isPasswordValid(_user.Password, out string error);


            // ASSERT
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