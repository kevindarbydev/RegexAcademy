using HamburgerMenu;
using System;
using System.Windows;

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
        }

        private void HbMenuItem_Click(object sender, RoutedEventArgs e)
        {

            HamburgerMenuItem hmi = sender as HamburgerMenuItem;
            switch (hmi.Text)
            {
                case "Students":
                    //ContentFrame.Content = new TextBlock { Text = "Button 1 clicked" };
                    ContentFrame.Source = new Uri("StudentWindow.xaml", UriKind.Relative);

                    break;
                case "Teachers":
                    ContentFrame.Source = new Uri("TeacherWindow.xaml", UriKind.Relative);
                    break;
                case "Courses":
                    ContentFrame.Source = new Uri("CoursesWindow.xaml", UriKind.Relative);
                    break;
                case "StudentCourses":
                    ContentFrame.Source = new Uri("StudentCoursesWindow.xaml", UriKind.Relative);
                    break;
            }
        }
    }

    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {

        }
    }
}
