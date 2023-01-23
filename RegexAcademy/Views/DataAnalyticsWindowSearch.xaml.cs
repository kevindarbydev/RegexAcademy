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


        private void TbxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //properly resets list
                if (TbxSearchBar.Text == "")
                {
                    LvSearchResults.ItemsSource = null;
                    return;
                }

                List<Student> studentsMatched = Globals.dbContext.Students.Where(s => s.FirstName.StartsWith(TbxSearchBar.Text)).ToList();

                genericMatched.Clear();
                genericMatched.AddRange(studentsMatched);

                LvSearchResults.ItemsSource = genericMatched;
            }
            catch (SystemException ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}, {ex.GetType().Name}");
            }
        }

    }
}
