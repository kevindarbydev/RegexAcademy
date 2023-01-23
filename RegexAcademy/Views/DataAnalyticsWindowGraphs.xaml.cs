using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for DataAnalyticsWindowGraphs.xaml
    /// </summary>
    public partial class DataAnalyticsWindowGraphs : Page
    {

        public SeriesCollection graphData { get; set; }
        public DataAnalyticsWindowGraphs()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ColumnSeries coursesPerWeek = new ColumnSeries
                {
                    Title = "Courses Per Day",
                    Values = new ChartValues<int>()
                };
                graphData = new SeriesCollection { coursesPerWeek };
                ChartCoursesPerWeek.Series = graphData;

                //hold the num of courses per day in dict
                Dictionary<string, int> coursesPerDay = new Dictionary<string, int>();

                //iterate over all courses, if a course is on a unique day then add to dict, else increment count             
                List<string> daysSplit = new List<string>();
                foreach (var course in Globals.dbContext.Courses.ToList())
                {
                    //split each day from Weekday field ("Monday Wednesday" -> "Monday", "Wednesday")
                    string[] parts = course.Weekday.Split(' ');
                    if (parts.Length > 2)
                    {
                        //Weekday string suddenly began including commas, so the below code is a workaround for that
                        foreach (string day in parts)
                        {
                            for (int i = 0; i < day.Length; i++)
                            {
                                char current = day[i];
                                if (current == ',')
                                {
                                    continue;
                                }
                            }
                            if (day == " " || day == "") continue;
                            if (coursesPerDay.ContainsKey(day))
                            {
                                coursesPerDay[day]++;
                            }
                            else
                            {
                                coursesPerDay.Add(day, 1);
                            }
                        }
                    }
                    else if (coursesPerDay.ContainsKey(parts[0]))
                    { // only 1 day
                        coursesPerDay[parts[0]]++;
                    }
                    else coursesPerDay.Add(parts[0], 1);
                }

                coursesPerWeek.Values = new ChartValues<int>(coursesPerDay.Values);

                ChartCoursesPerWeek.AxisX.Add(new Axis
                {
                    Title = "Days of the Week",
                    Labels = new List<string>(coursesPerDay.Keys)
                });

                ChartCoursesPerWeek.AxisY.Add(new Axis
                {
                    Title = "Number of courses",
                    MinValue = 0,
                });

                coursesPerWeek.Fill = new SolidColorBrush(Color.FromRgb(55, 55, 254));
                coursesPerWeek.StrokeThickness = 2;

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Something went wrong (Courses table): " + ex.Message, "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnNextGraph_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //clear previous data and change header/axes data
                ChartCoursesPerWeek.Series.Clear();
                ChartCoursesPerWeek.AxisX.Remove(ChartCoursesPerWeek.AxisX[0]);
                ChartCoursesPerWeek.AxisY.Remove(ChartCoursesPerWeek.AxisY[0]);
                ColumnSeries studentsPerClass = new ColumnSeries
                {
                    Title = "Students Per Class",
                    Values = new ChartValues<int>()
                };
                LblGraphHeader.Content = "Students per class";
                graphData = new SeriesCollection { studentsPerClass };
                ChartCoursesPerWeek.Series = graphData;


                var countOfStudents = (from sc in Globals.dbContext.StudentCourses
                                       join c in Globals.dbContext.Courses on sc.CourseId equals c.CourseId
                                       group sc by c.CourseName into g
                                       select new { CourseName = g.Key, count = g.Count() }).ToList();



                ChartCoursesPerWeek.AxisX.Add(new Axis
                {
                    Title = "Course",
                });

                ChartCoursesPerWeek.AxisY.Add(new Axis
                {
                    Title = "Number of students",
                    MinValue = 0,
                });

                foreach (var item in countOfStudents)
                {
                    var column = new ColumnSeries
                    {
                        Title = item.CourseName,
                        Values = new ChartValues<int> { item.count }
                    };
                    graphData.Add(column);
                }
                BtnNextGraph.Visibility = Visibility.Hidden;
                BtnLastGraph.Visibility = Visibility.Visible;
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLastGraph_Click(object sender, RoutedEventArgs e)
        {//code duplication, would improve if more time
            try
            {
                //clear previous data
                ChartCoursesPerWeek.Series.Clear();
                ChartCoursesPerWeek.AxisX.Remove(ChartCoursesPerWeek.AxisX[0]);
                ChartCoursesPerWeek.AxisY.Remove(ChartCoursesPerWeek.AxisY[0]);
                LblGraphHeader.Content = "Courses per day";
                ColumnSeries coursesPerWeek = new ColumnSeries
                {
                    Title = "Courses Per Day",
                    Values = new ChartValues<int>()
                };
                graphData = new SeriesCollection { coursesPerWeek };
                ChartCoursesPerWeek.Series = graphData;

                //hold the num of courses per day in dict
                Dictionary<string, int> coursesPerDay = new Dictionary<string, int>();

                //iterate over all courses, if a course is on a unique day then add to dict, else increment count
                //split each day from Weekday field ("Monday Wednesday")
                List<string> daysSplit = new List<string>();
                foreach (var course in Globals.dbContext.Courses.ToList())
                {
                    string[] parts = course.Weekday.Split(' ');
                    if (parts.Length > 2)
                    {

                        foreach (string day in parts)
                        {
                            if (day == " " || day == "") continue;
                            if (coursesPerDay.ContainsKey(day))
                            {
                                coursesPerDay[day]++;
                            }
                            else
                            {
                                coursesPerDay.Add(day, 1);
                            }
                        }
                    }
                    else if (coursesPerDay.ContainsKey(parts[0]))
                    { // only 1 day
                        coursesPerDay[parts[0]]++;
                    }
                    else coursesPerDay.Add(parts[0], 1);
                }

                coursesPerWeek.Values = new ChartValues<int>(coursesPerDay.Values);

                ChartCoursesPerWeek.AxisX.Add(new Axis
                {
                    Title = "Days of the Week",
                    Labels = new List<string>(coursesPerDay.Keys)
                });

                ChartCoursesPerWeek.AxisY.Add(new Axis
                {
                    Title = "Number of courses",
                    MinValue = 0,
                });

                coursesPerWeek.Fill = new SolidColorBrush(Color.FromRgb(55, 55, 254));
                coursesPerWeek.StrokeThickness = 2;
                BtnNextGraph.Visibility = Visibility.Visible;
                BtnLastGraph.Visibility = Visibility.Hidden;

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Something went wrong (Courses table): " + ex.Message, "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
