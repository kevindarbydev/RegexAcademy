using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;

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
            TeacherAdd inputDialog = new TeacherAdd();
            if (inputDialog.ShowDialog() == true)
            {
                MessageBox.Show("Add Teacher Window was closed");
                LvTeachers.ItemsSource = Globals.dbContext.Teachers.ToList();
            }
        }
    }
}
