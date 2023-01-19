using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Page
    {
        public StudentWindow()
        {
            InitializeComponent();
        }



        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd newStudent = new StudentAdd();
            if (newStudent.ShowDialog() == true)
            {
                MessageBox.Show("Add Teacher Window was closed");
            }
        }
    }
}
