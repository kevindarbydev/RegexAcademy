using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using RegexAcademy.Models;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Page
    {
        public TeacherWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext(); // Exceptions
                LvTeachers.ItemsSource = Globals.dbContext.Teachers.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void LvTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherAddEdit inputDialog = new TeacherAddEdit();
            if (inputDialog.ShowDialog() == true)
            {
                LvTeachers.ItemsSource = Globals.dbContext.Teachers.ToList();
            }
        }

        private void BtnEditTeacher_Click(object sender, RoutedEventArgs e)
        {
            Teacher currSelTeacher = LvTeachers.SelectedItem as Teacher;
            TeacherAddEdit inputDialog = new TeacherAddEdit(currSelTeacher);
            if (inputDialog.ShowDialog() == true)
            {
                LvTeachers.ItemsSource = Globals.dbContext.Teachers.ToList();
            }
        }

        private void BtnDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Teacher currSelTeacher = LvTeachers.SelectedItem as Teacher;
                if (currSelTeacher == null) return;

                var result = MessageBox.Show("Are you sure you want to unregister the following teacher?\n" + currSelTeacher, "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;
                Globals.dbContext.Teachers.Remove(currSelTeacher);
                Globals.dbContext.SaveChanges(); // ex SystemException
                LvTeachers.ItemsSource = Globals.dbContext.Teachers.ToList();
                LvTeachers.SelectedIndex = -1;
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
