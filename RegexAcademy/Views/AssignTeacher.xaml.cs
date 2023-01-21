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
        private Teacher assignedTeacher = null;

        List<object> genericMatched = new List<object>(); // List used to show which students match the search bar

        public AssignTeacher(Course selectedCourse)
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext();

                allTeachers = Globals.dbContext.Teachers.ToList();
                LvAllTeachers.ItemsSource = allTeachers;
                if (selectedCourse.TeacherId != null)
                {
                    assignedTeacher = Globals.dbContext.Teachers.Where(t => t.Id == selectedCourse.TeacherId).FirstOrDefault();
                    LblFirstAndLastName.Content = $"Currently Assigned Teacher: \n {assignedTeacher.FirstName} {assignedTeacher.LastName}";
                    ImgProfileImage.Source = assignedTeacher.ProfileImageToShow;
                }
               
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            if (selectedCourse != null)
            {
                this.selectedCourse = selectedCourse;
                LblCourseCode.Content = selectedCourse.CourseId;
            }
        }

        private void BtnAddToCourse_Click(object sender, RoutedEventArgs e)
        {
            assignedTeacher = LvAllTeachers.SelectedItem as Teacher;
            Globals.dbContext.Courses.Where(t => t.CourseId == selectedCourse.CourseId).FirstOrDefault().TeacherId = assignedTeacher.Id; // setting TeacherId in course entity

            if (assignedTeacher == null) { return; }

            LblFirstAndLastName.Content = $"Currently Assigned Teacher: \n {assignedTeacher.FirstName} {assignedTeacher.LastName}";
            ImgProfileImage.Source = assignedTeacher.ProfileImageToShow;

            selectedCourse.TeacherId = assignedTeacher.Id;
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            
            //Globals.dbContext.SaveChanges(); // SystemException

            //MessageBox.Show(this, "Teacher has been assigned", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            //this.Close();
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
