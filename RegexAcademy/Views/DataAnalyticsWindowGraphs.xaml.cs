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
        public DataAnalyticsWindowGraphs()
        {
            InitializeComponent();

        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ColumnSeries coursesPerWeek = new ColumnSeries
                {
                    Title = "Courses per day",
                    Values = new ChartValues<int>()
                };
                ChartCoursesPerWeek.Series = new SeriesCollection { coursesPerWeek };
                //ChartCoursesPerWeek.Series.Add(coursesPerWeek);

                //hold the num of courses per day in dict
                Dictionary<string, int> coursesPerDay = new Dictionary<string, int>();

                //iterate over all courses, if a course is on a unique day then add to dict, else increment count
                foreach (var course in Globals.dbContext.Courses.ToList())
                {
                    if (coursesPerDay.ContainsKey(course.Weekday))
                    {
                        coursesPerDay[course.Weekday]++;
                    }
                    else
                    {
                        coursesPerDay.Add(course.Weekday, 1);
                    }
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

                coursesPerWeek.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                coursesPerWeek.StrokeThickness = 2;
                ChartCoursesPerWeek.LegendLocation = LegendLocation.Right;

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
