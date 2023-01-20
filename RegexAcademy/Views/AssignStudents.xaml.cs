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
    /// Interaction logic for AssignStudents.xaml
    /// </summary>
    public partial class AssignStudents : Window
    {
        private Course selectedCourse = null;

        public AssignStudents()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext();
                List<Student> allStudents = Globals.dbContext.Students.ToList();
                List<Student> studentsInCourse = new List<Student>();
                LvStudentsNotInCourse.ItemsSource = allStudents;
                LvStudentsInCourse.ItemsSource = studentsInCourse;

            }
            catch(SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        public AssignStudents(Course selectedCourse)
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext();
                List<Student> allStudents = Globals.dbContext.Students.ToList();
                List<Student> studentsInCourse = new List<Student>();
                LvStudentsNotInCourse.ItemsSource = allStudents;
                LvStudentsInCourse.ItemsSource = studentsInCourse;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
            if(selectedCourse != null )
            {
                this.selectedCourse = selectedCourse;
                LblCourseCode.Content = selectedCourse.CourseId;
            }
        }
    }
}
