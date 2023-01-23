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
using System.Drawing;
using System.ComponentModel;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkWFpGaV1AQmFJfFBmRGJTfld6d1BWESFaRnZdQV1iS31Sd0diW31eeXdd;Mgo+DSMBPh8sVXJ0S0J+XE9AclRAQmFLYVF2R2BJfl96dVxMY1lBNQtUQF1hSn5SdEZiWH1fcnZURmdU;ORg4AjUWIQA/Gnt2VVhkQlFacltJX3xLfEx0RWFab19xflRPal9UVAciSV9jS31Td0dgWH5ccXZXR2JdWQ==;OTkwMzMzQDMyMzAyZTM0MmUzMEZpT0tGaXZheUYvS0RsMDdwdWJXWHZnV2NyWlpVdk9sWS8raGpRcG1aS3c9;OTkwMzM0QDMyMzAyZTM0MmUzMFR0dXQrTFF3WS9YUHk4aGpQVDhVTVB3MnVtdHlYSzdiNzdaT3doR3VJems9;NRAiBiAaIQQuGjN/V0Z+WE9EaFtGVmFWfFFpR2NbfE5xdV9HaVZSQWYuP1ZhSXxQdkRhWn5fcnVXRGFUUkw=;OTkwMzM2QDMyMzAyZTM0MmUzMGJiUFhiK1ljZmdQRTNPU1dKMnA3cnl6bWZsSTBNdHdYYTVwNEhSQ3pMaFU9;OTkwMzM3QDMyMzAyZTM0MmUzMFhkTnVXZFZBT1g3MHlIQm1KaEJDVjdWazcwdVlhTVR4NnFmTWZ0ZElQVU09;Mgo+DSMBMAY9C3t2VVhkQlFacltJX3xLfEx0RWFab19xflRPal9UVAciSV9jS31Td0dgWH5ccXZXRGNVWQ==;OTkwMzM5QDMyMzAyZTM0MmUzMGQveUZTVVhvbVdJWFdKSWhkWS9BQ0dUd0ZpQ1JhSjVHc245UFo1N251L009;OTkwMzQwQDMyMzAyZTM0MmUzMGxsdC9HWGl2Z2FOOWhvVmtmbWZMMXVJY0RTZVMybnNOakxuZDRqakNVVFk9;OTkwMzQxQDMyMzAyZTM0MmUzMGJiUFhiK1ljZmdQRTNPU1dKMnA3cnl6bWZsSTBNdHdYYTVwNEhSQ3pMaFU9");
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

            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf"; //just testing if exporting as csv works here --> not really --> each line is exported to a single cell

            if (saveFileDialog.ShowDialog() == true)
            {
                using (PdfDocument document = new PdfDocument())
                {
                    List<Student> students = Globals.dbContext.Students.ToList();
                    List<Teacher> teachers = Globals.dbContext.Teachers.ToList();
                    List<Course> courses = Globals.dbContext.Courses.ToList();
                    List<StudentCourse> studentCourses = Globals.dbContext.StudentCourses.ToList();

                    DateTime today = DateTime.Now;

                    StringBuilder sb = new StringBuilder();

                    PdfPage page = document.Pages.Add();
                    PdfGraphics graphics = page.Graphics;
                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 6);

                    sb.AppendLine($"Data Exported on {today}:\n");
                    sb.AppendLine("All Students: ");
                    
                    foreach (Student student in students)
                    {
                        sb.AppendLine(student.ToString());
                        
                    }

                    sb.AppendLine("\nAll Teachers:");
                    
                    foreach (Teacher teacher in teachers)
                    {
                        sb.AppendLine(teacher.ToString());
                        
                    }

                    sb.AppendLine("\nAll Courses:");
                    
                    foreach (Course course in courses)
                    {
                        sb.AppendLine(course.ToString());
                    }

                    sb.AppendLine("\nStudents Currently in Courses:");
                    
                    foreach (StudentCourse studentCourse in studentCourses)
                    {
                        sb.AppendLine(studentCourse.ToString());
                    }
                    
                    graphics.DrawString(sb.ToString(), font, PdfBrushes.Black, new PointF(0, 0));

                    document.Save(saveFileDialog.FileName);
                }
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
