using CsvHelper;
using Microsoft.Win32;
using RegexAcademy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for DataAnalyticsDashboard.xaml
    /// </summary>
    public partial class DataAnalyticsDashboard : Page
    {
        public DataAnalyticsDashboard()
        {
            InitializeComponent();
        }

        private void BtnSearchRecords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindowSearch.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }



        private void BtnOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindowGraphs.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }

        private void BtnViewStats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindowStatistics.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }

        private void BtnExportToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*|Data Files (*.data)|*.data|CSV (*.csv)|*.csv"; //just testing if exporting as csv works here --> not really --> each line is exported to a single cell

            if (saveFileDialog.ShowDialog() == true)
            {
                List<Student> students = Globals.dbContext.Students.ToList();
                List<Teacher> teachers = Globals.dbContext.Teachers.ToList();
                List<Course> courses = Globals.dbContext.Courses.ToList();
                List<StudentCourse> studentCourses = Globals.dbContext.StudentCourses.ToList();

                DateTime today = DateTime.Now;

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"Data Exported on {today}:\n");
                sb.AppendLine("All Students: ");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (Student student in students)
                {
                    sb.AppendLine(student.ToString());
                    //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                }

                sb.AppendLine("\nAll Teachers:");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (Teacher teacher in teachers)
                {
                    sb.AppendLine(teacher.ToString());
                    //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                }

                sb.AppendLine("\nAll Courses:");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (Course course in courses)
                {
                    sb.AppendLine(course.ToString());
                    //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                }

                sb.AppendLine("\nStudents Currently in Courses:");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (StudentCourse studentCourse in studentCourses)
                {
                    sb.AppendLine(studentCourse.ToString());
                }
                File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }

        }

        private void BtnExportToFileCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "CSV (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {

                List<Student> students = Globals.dbContext.Students.ToList();
                List<Teacher> teachers = Globals.dbContext.Teachers.ToList();
                List<Course> courses = Globals.dbContext.Courses.ToList();
                List<StudentCourse> studentCourses = Globals.dbContext.StudentCourses.ToList();

                // unfortunately this doesn't work 
                // List<object> allRecords = new List<object>();

                //foreach (Student s in students)
                //{
                //    allRecords.Add(s);
                //}
                //foreach (Teacher t in teachers)
                //{
                //    allRecords.Add(t);
                //}
                //foreach (Course c in courses)
                //{
                //    allRecords.Add(c);
                //}
                //foreach (StudentCourse sc in studentCourses)
                //{
                //    allRecords.Add(sc);
                //}

                using (var writer = new StreamWriter(saveFileDialog.FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(students);
                }



            }

        }
    }
}
