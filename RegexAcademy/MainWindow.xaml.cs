using HamburgerMenu;
using System;
using System.ComponentModel;
using System.Windows;
using RegexAcademy.Views;

namespace RegexAcademy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Globals.dbContext = new RegexAcademyDbContext(); // Exceptions
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

            if (Globals.isLoggedIn == false)
            {
                Register RegDlg = new Register();
                RegDlg.ShowDialog();
            }
        }

        private void HbMenuItem_Click(object sender, RoutedEventArgs e)
        {

            HamburgerMenuItem hmi = sender as HamburgerMenuItem;
            switch (hmi.Text)
            {
                case "Students":
                    //ContentFrame.Content = new TextBlock { Text = "Button 1 clicked" };
                    ContentFrame.Source = new Uri("Views/StudentWindow.xaml", UriKind.Relative);

                    break;
                case "Teachers":
                    ContentFrame.Source = new Uri("Views/TeacherWindow.xaml", UriKind.Relative);
                    break;
                case "Courses":
                    ContentFrame.Source = new Uri("Views/CoursesWindow.xaml", UriKind.Relative);
                    break;
                case "StudentCourses":
                    ContentFrame.Source = new Uri("Views/StudentCoursesWindow.xaml", UriKind.Relative);
                    break;

                default:
                    Console.WriteLine("Internal Error - HbMenuItem_Click has failed");
                    break;
            }
        }

        // removed hook so that it's not annoying during development.
        // to re-enable, add following line to MainWindow.xaml in <Window> element:
        // Closing='Window_Closing'
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(this, "Are you sure you wish to exit?", "Exit", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Environment.Exit(0);
                    break;
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;

            }
        }


    }
}
