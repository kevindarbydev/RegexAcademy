using RegexAcademy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for DataAnalyticsWindow.xaml
    /// </summary>
    public partial class DataAnalyticsWindow : Page
    {

        List<object> genericMatched = new List<object>();
        public DataAnalyticsWindow()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TbxSearchBar.Text == "") return;
            string searchString = TbxSearchBar.Text;

            //iterate through Students and Teachers, only matching first name for now
            //TODO: match by first name, last name, AND combination (All 3). some helper methods in Student class will help
            List<Student> studentsMatched = Globals.dbContext.Students.Where(s => s.FirstName.Contains(searchString)).ToList();
            List<Teacher> teachersMatched = Globals.dbContext.Teachers.Where(t => t.FirstName.Contains(searchString)).ToList();

            genericMatched.Clear(); //clear previous results
            genericMatched.AddRange(teachersMatched);
            genericMatched.AddRange(studentsMatched);

            LvSearchResults.ItemsSource = genericMatched;

        }
    }
}
