using RegexAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
                    LblFirstAndLastName.Content = $"Currently Assigned Teacher \n\n           {assignedTeacher.FirstName} {assignedTeacher.LastName}";
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
                LblCourseCode.Content = $"{selectedCourse.CourseId} > {selectedCourse.CourseName}";
            }
        }

        private void BtnAddToCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                assignedTeacher = LvAllTeachers.SelectedItem as Teacher;
                if (assignedTeacher.Availability == false)
                {
                    MessageBox.Show("Sorry, this teacher is unavailable for work!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Globals.dbContext.Courses.Where(t => t.CourseId == selectedCourse.CourseId).FirstOrDefault().TeacherId = assignedTeacher.Id; // setting TeacherId in course entity

                if (assignedTeacher == null) { return; }

                LblFirstAndLastName.Content = $"Currently Assigned Teacher \n\n           {assignedTeacher.FirstName} {assignedTeacher.LastName}";
                ImgProfileImage.Source = assignedTeacher.ProfileImageToShow;

                selectedCourse.TeacherId = assignedTeacher.Id;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnRemoveFromCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext.Courses.Where(t => t.CourseId == selectedCourse.CourseId).FirstOrDefault().TeacherId = null;
                LblFirstAndLastName.Content = $"Currently Assigned Teacher";
                ImgProfileImage.Source = null;
                selectedCourse.TeacherId = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext.SaveChanges(); // SystemException

                MessageBox.Show(this, "Teacher has been updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            selectedCourse.TeacherId = null;
            MessageBoxResult result = MessageBox.Show(this, "Are you sure you wish to exit?\n All changes will be lost.", "Exit", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    //e.Cancel = true;
                    break;
                case MessageBoxResult.Cancel:
                    //e.Cancel = true;
                    break;
                default:
                    Console.WriteLine("Internal Error - Window_Closing() has failed");
                    break;
            }
        }


    }
}
