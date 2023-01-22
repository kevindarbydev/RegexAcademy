using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Page
    {
        public StudentWindow()
        {
            InitializeComponent();
            LvStudents.ItemsSource = Globals.dbContext.Students.ToList();
        }



        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd newStudent = new StudentAdd();
            if (newStudent.ShowDialog() == false)
            {
                // MessageBox.Show("Add Student Window was closed");
                LvStudents.ItemsSource = Globals.dbContext.Students.ToList();
            }
        }

        private void LvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = LvStudents.SelectedItem as Models.Student;
            if (student == null) return;
            // MessageBox.Show(student.ToString());
        }

        private void BtnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LvStudents.SelectedItem != null)
                {
                    StudentAdd newStudent = new StudentAdd((Models.Student)LvStudents.SelectedItem); //calling secondary constructor, which loads slightly different XAML to facilitate editing rather than adding

                    if (newStudent.ShowDialog() == false)
                    {
                        // MessageBox.Show("Add Student Window was closed");
                        LvStudents.ItemsSource = Globals.dbContext.Students.ToList();
                    }
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is SystemException)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDltStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var student = LvStudents.SelectedItem as Models.Student;
                if (student == null) return;
                Models.Student toBeDeleted = Globals.dbContext.Students.Where(s => s.Id == student.Id).FirstOrDefault();
                Globals.dbContext.Students.Remove(toBeDeleted);
                Globals.dbContext.SaveChanges();
                LvStudents.ItemsSource = Globals.dbContext.Students.ToList();
                LblDisplayMsgToUser.Content = $"Deleted the following record: {student}";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void MiSortId_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LvStudents.ItemsSource = Globals.dbContext.Students.OrderBy(s => s.Id).ToList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiSortFName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LvStudents.ItemsSource = Globals.dbContext.Students.OrderBy(s => s.FirstName).ToList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiSortLName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LvStudents.ItemsSource = Globals.dbContext.Students.OrderBy(s => s.LastName).ToList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiSortDob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LvStudents.ItemsSource = Globals.dbContext.Students.OrderBy(s => s.DateOfBirth).ToList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message} ({ex.GetType().Name})", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
