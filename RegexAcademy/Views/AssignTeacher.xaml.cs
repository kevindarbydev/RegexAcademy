using RegexAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for AssignTeacher.xaml
    /// </summary>
    public partial class AssignTeacher : Window
    {
        private Course selectedCourse = null;

        List<Teacher> allTeachers = new List<Teacher>();
        List<Teacher> teacherTeachingCourse = new List<Teacher>(); // may not have to be a list since only one teacher can teach a course

        List<object> genericMatched = new List<object>(); // List used to show which students match the search bar

        public AssignTeacher(Course selectedTeacher)
        {
            InitializeComponent();
            //try
            //{
            //    Globals.dbContext = new RegexAcademyDbContext();
            //    allTeachers = Globals.dbContext.Teachers.ToList();

            //    var studentCourseList = Globals.dbContext.StudentCourses.Where(sc => sc.CourseId == selectedCourse.CourseId).ToList();

            //    foreach (StudentCourse studentCourse in studentCourseList)
            //    {
            //        Student student = Globals.dbContext.Students.Find(studentCourse.StudentId);
            //        studentsInCourse.Add(student);

            //        allStudents.Remove(student);
            //    }

            //    LvAllStudents.ItemsSource = allStudents;
            //    LvStudentsInCourse.ItemsSource = studentsInCourse;
            //}
            //catch (SystemException ex)
            //{
            //    MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    this.Close();
            //}
            //if (selectedCourse != null)
            //{
            //    this.selectedCourse = selectedCourse;
            //    LblCourseCode.Content = selectedCourse.CourseId;
            //    LblStudentsInCourse.Content = $"Students in Course {selectedCourse.CourseId}:";
            //}
        }

        private void BtnAddToCourse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemoveFromCourse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      

       

       
    }
}
