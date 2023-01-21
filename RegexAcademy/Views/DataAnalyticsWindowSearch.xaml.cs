using RegexAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for DataAnalyticsWindow.xaml
    /// </summary>
    public partial class DataAnalyticsWindowSearch : Page
    {

        List<object> genericMatched = new List<object>();
        public DataAnalyticsWindowSearch()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //TODO:
                //get list to refresh went search is pressed again
                //LvSearchResults.ItemsSource = null;
                if (TbxSearchBar.Text == "") return;
                string searchString = TbxSearchBar.Text;


                //iterate through Students and Teachers                
                List<Student> studentsMatched = Globals.dbContext.Students.Where(s => s.FirstName.Contains(searchString)).ToList();
                List<Teacher> teachersMatched = Globals.dbContext.Teachers.Where(t => t.FirstName.Contains(searchString)).ToList();

                studentsMatched.AddRange(Globals.dbContext.Students.Where(s => s.LastName.Contains(searchString)).ToList());
                teachersMatched.AddRange(Globals.dbContext.Teachers.Where(t => t.LastName.Contains(searchString)).ToList());

                //TODO: implement below
                //studentsMatched.AddRange(Globals.dbContext.Students.Where(s => s.NameConcatenation().Contains(searchString)).ToList());
                //teachersMatched.AddRange(Globals.dbContext.Teachers.Where(t => t.NameConcatenation().Contains(searchString)).ToList());

                genericMatched.Clear(); //clear previous results
                genericMatched.AddRange(teachersMatched);
                genericMatched.AddRange(studentsMatched);

                LvSearchResults.ItemsSource = genericMatched;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.GetType().Name);
            }
        }
    }
}
