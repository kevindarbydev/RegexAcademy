using RegexAcademy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
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

        List<Student> allStudents = new List<Student>();
        List<Student> studentsInCourse = new List<Student>();

        List<object> genericMatched = new List<object>(); // List used to show which students match the search bar

        public AssignStudents()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext();
                allStudents = Globals.dbContext.Students.ToList();

                var studentCourseList = Globals.dbContext.StudentCourses.Where(sc => sc.CourseId == selectedCourse.CourseId).ToList();

                foreach(StudentCourse studentCourse in studentCourseList)
                {
                    Student student = Globals.dbContext.Students.Find(studentCourse.StudentId);
                    studentsInCourse.Add(student);

                    allStudents.Remove(student);
                }

                LvAllStudents.ItemsSource = allStudents;
                LvStudentsInCourse.ItemsSource = studentsInCourse;

            }
            catch(SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        public AssignStudents(Course selectedCourse)
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext();
                allStudents = Globals.dbContext.Students.ToList();

                var studentCourseList = Globals.dbContext.StudentCourses.Where(sc => sc.CourseId == selectedCourse.CourseId).ToList();

                foreach (StudentCourse studentCourse in studentCourseList)
                {
                    Student student = Globals.dbContext.Students.Find(studentCourse.StudentId);
                    studentsInCourse.Add(student);

                    allStudents.Remove(student);
                }

                LvAllStudents.ItemsSource = allStudents;
                LvStudentsInCourse.ItemsSource = studentsInCourse;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            if(selectedCourse != null )
            {
                this.selectedCourse = selectedCourse;
                LblCourseCode.Content = selectedCourse.CourseId;
                LblStudentsInCourse.Content = $"Students in Course {selectedCourse.CourseId}:";
            }
        }

        private void BtnAddToCourse_Click(object sender, RoutedEventArgs e)
        {
            List<Student> selectedStudents = LvAllStudents.SelectedItems.Cast<Student>().ToList();

            if(selectedStudents == null) { return; }

            foreach(Student student in selectedStudents)
            {
                allStudents.Remove(student);
                studentsInCourse.Add(student);
            }

            LvAllStudents.Items.Refresh();
            LvStudentsInCourse.Items.Refresh();
        }

        private void BtnRemoveFromCourse_Click(object sender, RoutedEventArgs e)
        {
            List<Student> selectedStudents = LvStudentsInCourse.SelectedItems.Cast<Student>().ToList();

            if(selectedStudents == null ) { return; }

            foreach(Student student in selectedStudents)
            {
                studentsInCourse.Remove(student);
                allStudents.Add(student);
            }

            LvStudentsInCourse.Items.Refresh();
            LvAllStudents.Items.Refresh();
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<StudentCourse> studentCourseList = Globals.dbContext.StudentCourses.Where(sc => sc.CourseId == selectedCourse.CourseId).ToList();
                List<Student> students = studentsInCourse;

                if (students == null) { return; }

                foreach (Student student in students)
                {
                    StudentCourse foundStudentCourse = studentCourseList.Find(sc => sc.StudentId == student.Id);
                    
                    if(foundStudentCourse != null)
                    {
                        studentCourseList.Remove(foundStudentCourse);
                        continue;
                    }

                    if (foundStudentCourse == null)
                    {
                        StudentCourse studentCourse = new StudentCourse { CourseId = selectedCourse.CourseId, StudentId = student.Id, Grade = 0 };
                        Globals.dbContext.StudentCourses.Add(studentCourse);
                    }
                }

                foreach(StudentCourse sc in studentCourseList)
                {
                    Student foundStudent = students.Find(s => s.Id == sc.Id);

                    if(foundStudent != null) 
                    {
                        continue;
                    }

                    if(foundStudent == null)
                    {
                        Globals.dbContext.StudentCourses.Remove(sc);
                    }
                }
               
                Globals.dbContext.SaveChanges(); // SystemException

                MessageBox.Show(this, "Student assignments have been saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch(SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelChanges_Click(object sender, RoutedEventArgs e)
        {
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

        private void TbxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TbxSearchBar.Text == "")
            {
                LvAllStudents.ItemsSource = allStudents;
                return;
            }

            List<Student> studentsMatched = Globals.dbContext.Students.Where(s => s.FirstName.StartsWith(TbxSearchBar.Text)).ToList();

            genericMatched.Clear();
            genericMatched.AddRange(studentsMatched);

            LvAllStudents.ItemsSource = genericMatched;
        }
    }
}
