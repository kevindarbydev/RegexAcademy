using System;
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
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindow.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }
    }
}
